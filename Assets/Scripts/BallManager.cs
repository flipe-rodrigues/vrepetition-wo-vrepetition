using System;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject tennisBall; // Assign your tennis ball prefab in the Inspector
    public Vector3 initialVelocity = new Vector3(0, 1, -1); // Initial velocity of the ball
    public float spawnInterval = 2.0f; // Time between spawns

    public event Action<GameObject> OnBallSpawned; // Event to notify when a ball is spawned

    private float _timer;


    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            SpawnTennisBall();
            _timer = 0f;
        }
    }

    void SpawnTennisBall()
    {
        if (tennisBall != null)
        {
            tennisBall.transform.position = this.transform.position; // Set the position to the spawner's position

            Rigidbody rb = tennisBall.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 force = initialVelocity;

                rb.AddForce(force, ForceMode.Impulse);
                rb.AddTorque(force, ForceMode.Impulse);
            }

            OnBallSpawned?.Invoke(tennisBall); // Notify subscribers that a ball has been spawned

        }
        else
        {
            Debug.LogWarning("TennisBallPrefab or SpawnPoint is not assigned!");
        }
    }
}
