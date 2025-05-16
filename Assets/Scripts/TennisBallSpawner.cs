using UnityEngine;
using System;

public class TennisBallSpawner : MonoBehaviour
{
    public GameObject tennisBallPrefab;
    public Vector3 initialVelocity = new Vector3(0, 0.3f, -0.3f);
    public float spawnInterval = 5.0f;

    public event Action<GameObject> OnBallSpawned;

    private float _timer;
    private GameObject _currentBall;
    private Rigidbody _rb;
    private TrackingManager _trackingManager;
    private TrajectoryTrackerVR _ballTracker; // Refer�ncia ao tracker da bola

    void Start()
    {
        // Instancia a bola �nica
        _currentBall = Instantiate(tennisBallPrefab, transform.position, Quaternion.identity, transform);
        _currentBall.name = tennisBallPrefab.name; // Define o nome ANTES de desativar
        _currentBall.SetActive(false);
        _rb = _currentBall.GetComponent<Rigidbody>();

        // Obt�m o TrajectoryTracker da bola
        _ballTracker = _currentBall.GetComponent<TrajectoryTrackerVR>();
        if (_ballTracker == null)
        {
            Debug.LogError("A bola n�o tem um TrajectoryTracker associado!");
        }

        // Obt�m o TrackingManager
        _trackingManager = FindFirstObjectByType<TrackingManager>();
        if (_trackingManager == null)
        {
            Debug.LogError("TrackingManager n�o encontrado na cena!");
        }

        // Adiciona o tracker da bola � lista do TrackingManager
        if (_trackingManager != null && _ballTracker != null)
        {
            _trackingManager.trackingList.Add(_ballTracker);
        }
        else
        {
            Debug.LogError("N�o foi poss�vel adicionar o tracker da bola � lista do TrackingManager.");
        }
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
        if (_currentBall == null || _rb == null || _ballTracker == null) return;

        // Remove "(Clone)" do nome (se existir)
        // string cleanName = _currentBall.name.Replace("(Clone)", "");
        // _currentBall.name = cleanName; // Atualiza o nome do objeto

        // 1. Reset da bola
        _currentBall.SetActive(false);
        _currentBall.transform.position = transform.position;
        _rb.linearVelocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;

    
        // 3. Reativa e lan�a
        _currentBall.SetActive(true);
        _rb.AddForce(initialVelocity, ForceMode.Impulse);
        _rb.AddTorque(initialVelocity, ForceMode.Impulse);

        // 4. Notifica listeners
        OnBallSpawned?.Invoke(_currentBall);
    }

    void OnDestroy()
    {
        // Remove o TRACKER da lista ao destruir
        if (_trackingManager != null && _ballTracker != null)
        {
            _trackingManager.trackingList.Remove(_ballTracker);
        }
    }
}