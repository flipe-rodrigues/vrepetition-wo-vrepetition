using UnityEngine;

public class RacketBehavior : MonoBehaviour{
    [Header("Hit Settings")]
    public float minHitSpeed = 0.5f; // Minimum racket speed to add force
    public float baseHitPower = 5f;
    public float maxHitPower = 20f;
    public float velocityToPowerRatio = 0.5f;

    private Vector3 previousPosition;
    private Vector3 currentVelocity;

    void Update()
    {
        currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TennisBall"))
        {
            Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 racketFaceNormal = transform.forward;
            Vector3 incomingDirection = ballRb.linearVelocity.normalized;

            // Basic reflection (works when racket is stationary)
            Vector3 reflectedDirection = Vector3.Reflect(incomingDirection, racketFaceNormal);

            // Only add force if racket is moving significantly
            if (currentVelocity.magnitude > minHitSpeed)
            {
                // Calculate additional force from racket movement
                float racketSpeed = currentVelocity.magnitude;
                float hitStrength = Mathf.Min(
                    baseHitPower + (racketSpeed * velocityToPowerRatio),
                    maxHitPower
                );

                // Blend between pure reflection and added force
                Vector3 forceDirection = Vector3.Lerp(
                    reflectedDirection,
                    currentVelocity.normalized,
                    0.7f // 70% toward racket's movement direction
                ).normalized;

                ballRb.AddForce(forceDirection * hitStrength, ForceMode.VelocityChange);
            }
            else
            {
                // Just reflect with energy conservation (no added force)
                ballRb.linearVelocity = reflectedDirection * ballRb.linearVelocity.magnitude * 0.95f; // Small energy loss
            }
        }
    }
}

