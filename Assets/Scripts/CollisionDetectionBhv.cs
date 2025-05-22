
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(TrajectoryTrackerVR))]

public class CollisionDetectionBhv : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // O Unity só chama este método se as layers puderem colidir!
        TrackingManager.Instance?.LogCollision(gameObject.layer, collision.gameObject.layer);

    }
}