using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int markValue = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMarks.Instance.AddMarks(markValue);
            Destroy(gameObject);
        }
    }
}
