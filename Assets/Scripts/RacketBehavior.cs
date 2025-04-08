using UnityEngine;

public class RacketBehavior : MonoBehaviour{
    private Vector3 previousPosition;
    private float previousTime;
    public float forceMultiplier = 1.0f;

    public GameObject dummy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // update the previous position every frame
        previousPosition = transform.position;
        previousTime = Time.time;

        if (dummy != null)
        {
            dummy.transform.position = transform.position;
        }

       
    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("TennisBall"))
//        {
//           Rigidbody ballRb = collision.gameObject.GetComponent<Rigidbody>();

//            float deltaTime = Time.time - previousTime;

//            // calculate the racket's velocity based on movement since last frame
//            Vector3 velocity = (transform.position - previousPosition) / deltaTime;

//            // get the direction of impact
//            Vector3 impactDirection = collision.contacts[0].point - transform.position;

//            // Apply force to the ball in the direction of the racket's movement
//            ballRb.AddForce(velocity * forceMultiplier, ForceMode.Impulse);
//        }
//    }
}

