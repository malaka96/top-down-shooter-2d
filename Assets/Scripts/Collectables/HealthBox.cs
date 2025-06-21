using UnityEngine;

public class HealthBox : MonoBehaviour
{
    public float healAmount = 25f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.Heal(healAmount);
            }
            Destroy(gameObject);
        }
    }
}
