using UnityEngine;

public class AIFollowPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    [SerializeField] private float movementSpeed = 2.5f; // Movement speed of the enemy
    [SerializeField] private float attackRange = 1.5f; // Range within which the enemy can attack the player
    [SerializeField] private Animator animator; // Reference to the Animator component for animation events
    [SerializeField] private Rigidbody2D rb; // Reference to the Rigidbody2D component for movement
    [SerializeField] private int attackDamage = 10; // Amount of damage the enemy deals to the player
    [SerializeField] private float attackCooldown = 2f; // Cooldown time between attacks
    private Vector3 originalScale; // Original scale of the enemy game object

    private float distanceToPlayer; // Distance between the enemy and the player
    private float lastAttackTime = -999; // Time of the last attack, initialized to a large negative value
    private bool isMoving = false; // Flag to track if the enemy is moving

    public AudioSource enemyAttack; // Reference to the AudioSource component for attack sound

    void Start()
    {
        originalScale = transform.localScale; // Store the original scale of the enemy
    }

    void Update()
    {
        distanceToPlayer = Vector2.Distance(transform.position, player.position); // Calculate the distance to the player
        Vector2 direction = (player.position - transform.position).normalized; // Calculate the direction towards the player

        isMoving = distanceToPlayer > attackRange; // Set the isMoving flag based on the distance to the player

        animator.SetBool("IsMoving", isMoving); // Update the "IsMoving" animation parameter
        animator.SetFloat("Speed", isMoving ? direction.sqrMagnitude : 0f); // Update the "Speed" animation parameter

        if (distanceToPlayer <= attackRange && Time.time >= lastAttackTime + attackCooldown) // Check if the player is within attack range and the attack cooldown has elapsed
        {
            Attack(); // Call the Attack method
            lastAttackTime = Time.time; // Update the lastAttackTime
        }
    }

    void FixedUpdate()
    {
        if (distanceToPlayer > attackRange)
        {
            MoveCharacter((player.position - transform.position).normalized); // Move the enemy towards the player
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop the enemy's movement if within attack range
        }
    }

    void MoveCharacter(Vector2 direction)
    {
        if (direction.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Flip the enemy to the left
        else if (direction.x > 0)
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); // Flip the enemy to the right

        rb.MovePosition(rb.position + direction * movementSpeed * Time.fixedDeltaTime); // Move the enemy using the Rigidbody2D
    }

    void Attack()
    {
        animator.SetTrigger("Attack"); // Trigger the "Attack" animation
        enemyAttack.Play(); // Play the attack sound

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>(); // Get the PlayerHealth component from the player
        if (playerHealth != null && !playerHealth.IsDead()) // Check if the player is not dead
        {
            playerHealth.TakeDamage(attackDamage); // Deal damage to the player
        }
    }
}