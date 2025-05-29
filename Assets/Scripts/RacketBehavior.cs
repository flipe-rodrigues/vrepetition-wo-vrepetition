using UnityEngine;

public class RacketBehavior : MonoBehaviour
{
    public  float forceMultiplier = 2f;
    private Vector3 previousPosition;
    private Vector3 currentVelocity;

    void Update()
    {
        currentVelocity = (transform.position - previousPosition) / Time.deltaTime;
        previousPosition = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody ballRigidbody = collision.collider.GetComponent<Rigidbody>();

        if (ballRigidbody != null)
        {
            // Add velocity to the ball in the direction the racket is moving
            Vector3 hitDirection = currentVelocity.normalized;
            float hitStrength = currentVelocity.magnitude;


            ballRigidbody.linearVelocity = hitDirection * hitStrength * forceMultiplier;
        }
    }
}

