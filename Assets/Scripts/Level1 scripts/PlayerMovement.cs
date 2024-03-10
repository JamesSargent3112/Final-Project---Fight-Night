using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // Movement speed of the player
    [SerializeField] private Animator anim; // Reference to the Animator component
    [SerializeField] private Rigidbody2D rb; // Reference to the Rigidbody2D component
    [SerializeField] private Transform attackPoint; // Transform representing the attack point
    [SerializeField] private float attackRange = 1f; // Range of the player's attack
    [SerializeField] private LayerMask enemyLayer; // Layer mask to detect enemies
    [SerializeField] private PlayerHealth playerHealth; // Reference to the PlayerHealth script
    [SerializeField] private int attackDamage = 15; // Amount of damage the player deals
    public static PlayerMovement instance; // Singleton instance of the PlayerMovement class
    private Vector3 originalScale; // Original scale of the player game object

    private Vector2 movement; // Movement vector
    private bool isAttacking = false; // Flag to track if the player is attacking
    private float lastAttackTime = 0f; // Time of the last attack
    public float attackCooldown = 0.5f; // Cooldown time between attacks
    public AudioSource playerAttack; // Reference to the AudioSource component for attack sound

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        AIFollowPlayer[] aiFollowers = FindObjectsOfType<AIFollowPlayer>(); // Find all AIFollowPlayer components
        foreach (var follower in aiFollowers)
        {
            follower.player = this.transform; // Set the player transform for each AIFollowPlayer
        }

        originalScale = transform.localScale; // Store the original scale of the player
    }

    private void Update()
    {
        if (isAttacking || playerHealth.IsDead()) return; // Return if the player is attacking or dead

        movement.x = Input.GetAxisRaw("Horizontal"); // Get horizontal movement input
        movement.y = Input.GetAxisRaw("Vertical"); // Get vertical movement input

        anim.SetFloat("Speed", movement.sqrMagnitude); // Set the "Speed" animation parameter

        if (Input.GetButtonDown("Fire1") && Time.time >= lastAttackTime + attackCooldown) // Check for attack input and cooldown
        {
            StartCoroutine(AttackCoroutine()); // Start the attack coroutine
            lastAttackTime = Time.time; // Update the last attack time
        }
    }

    private void FixedUpdate()
    {
        if (!isAttacking && !playerHealth.IsDead()) // Move the player if not attacking and not dead
        {
            MoveCharacter(movement);
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Flip the player sprite to the right
        else if (direction.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Flip the player sprite to the left

        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime); // Move the player
    }

    private IEnumerator AttackCoroutine()
    {
        isAttacking = true; // Set the attacking flag to true
        anim.SetTrigger("Attack"); // Trigger the "Attack" animation
        playerAttack.Play(); // Play the attack sound

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer); // Detect enemies in attack range
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>(); // Get the EnemyHealth component from the enemy
            if (enemyHealth != null && !enemyHealth.isDead) // Check if the enemy is not dead
            {
                Debug.Log("We hit " + enemy.name); // Log the name of the enemy hit
                enemyHealth.TakeDamage(attackDamage); // Deal damage to the enemy
            }
        }

        yield return new WaitForSeconds(0.5f); // Wait for the attack animation to complete
        isAttacking = false; // Reset the attacking flag
    }
}