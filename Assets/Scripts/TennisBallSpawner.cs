using UnityEngine;
using System;

public class TennisBallSpawner : MonoBehaviour
{
    public GameObject tennisBall;
    public Vector3 initialVelocity = new Vector3(0, 0.3f, -0.3f);
    public float spawnInterval = 5.0f;

    public event Action<GameObject> OnBallSpawned;

    private float _timer;

    private Rigidbody _rb;


    void Start()
    {
        if (tennisBall == null)
        {
            Debug.LogError("TennisBall not assigned in inspector!");
            return;
        }

        _rb = tennisBall.GetComponent<Rigidbody>();
        if (_rb == null)
        {
            Debug.LogError("TennisBall has no Rigidbody component!");
        }

        // Initial launch
        LaunchBall();
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= spawnInterval)
        {
            LaunchBall();
            _timer = 0f;
        }
    }

    void LaunchBall()
    {
        if (tennisBall == null || _rb == null) return;

        // Reset position and physics
        tennisBall.transform.position = transform.position;
        tennisBall.SetActive(true); // Only need to call this once
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

        // Apply forces
        _rb.AddForce(initialVelocity, ForceMode.Impulse);
        _rb.AddTorque(initialVelocity, ForceMode.Impulse);

        // Notify listeners
        OnBallSpawned?.Invoke(tennisBall);
    }
}