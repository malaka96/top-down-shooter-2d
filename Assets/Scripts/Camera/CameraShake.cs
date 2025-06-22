using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.2f;

    private Vector3 initialPosition;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
    }

    public void Shake()
    {
        StopAllCoroutines();
        StartCoroutine(ShakeRoutine());
    }

    IEnumerator ShakeRoutine()
    {
        Debug.Log("called");
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector2 offset = Random.insideUnitCircle * shakeMagnitude;
            transform.localPosition = initialPosition + new Vector3(offset.x, offset.y, 0);

            elapsed += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localPosition = initialPosition;
    }
}
