using UnityEngine;

public class TennisBallSpawner : MonoBehaviour
{
    public GameObject tennisBallPrefab; // Assign your tennis ball prefab in the Inspector
    public Vector3 initialVelocity = new Vector3(0, 5, 10); // Initial velocity of the ball
    public float spawnInterval = 2.0f; // Time between spawns

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
        if (tennisBallPrefab != null)
        {
            GameObject newBall = Instantiate(tennisBallPrefab, this.transform.position, Quaternion.identity, this.transform);

            Rigidbody rb = newBall.GetComponent<Rigidbody>();

            if (rb != null)
            {
                Vector3 force = initialVelocity + new Vector3(Random.value, Random.value, Random.value);

                rb.AddForce(force, ForceMode.Impulse);
                rb.AddTorque(force, ForceMode.Impulse);
            }
        }
        else
        {
            Debug.LogWarning("TennisBallPrefab or SpawnPoint is not assigned!");
        }
    }
}
