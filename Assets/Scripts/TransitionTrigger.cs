using UnityEngine;

public class TransitionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && LevelManager.instance.canAdvance)
        {
            LevelManager.instance.AdvanceToNextSection();
        }
    }
}