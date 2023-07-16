using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public AnimationCurve cameraCurve;
    public float movementDuration = 1f;

    private Vector3 offset;
    private float timer = 0f;

    private void Start()
    {
        offset = transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = player.transform.position + offset;
        float interpolationValue = cameraCurve.Evaluate(Vector3.Distance(transform.position, targetPosition));

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / movementDuration);  // Normalize the timer within the movement duration

        // Apply interpolation value based on the normalized time
        float interpolatedValue = Mathf.Lerp(0f, interpolationValue, t);

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, interpolatedValue);
        transform.position = smoothedPosition;

        // Reset the timer if it exceeds the movement duration
        if (timer >= movementDuration)
        {
            timer = 0f;
        }
    }
}

