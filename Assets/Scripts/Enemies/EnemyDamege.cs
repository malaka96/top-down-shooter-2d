using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If enemy hits player
        if (collision.CompareTag("Player"))
        {
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }

            Die();
        }

        // If enemy is hit by a bullet
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject); // destroy bullet
            Die();
        }
    }

    void Die()
    {
        // Optional: play effect here
        Destroy(gameObject); // destroy self
    }
}
