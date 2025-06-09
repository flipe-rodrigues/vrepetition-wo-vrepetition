using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TaskManager : Singleton<TaskManager>
{
    // Trail variables

    public int TrialCounter { get; private set; } = 0;
    public int ValidTrialCounter { get; private set; } = 0;
    public bool IsTrialActive { get; private set; } = false;

    private GameObject _currentBall;

    // References

    public TennisBallSpawner ballSpawner;
    public TrackingManager trackingManager;

    public GameObject net;
    public MeshRenderer courtMeshRenderer;
    public GameObject zone;



    private void Start()
    {
        // adquirir a posição do Ball Spawnern e a velocidade da bola e logar no csv
        trackingManager.RecordEvent($"BallSpawnerPosition_{ballSpawner.transform.position}");
        trackingManager.RecordEvent($"BallInitialVelocity_{ballSpawner.initialVelocity}");

        // Registrar para os eventos de spawn da bola
        ballSpawner.OnBallSpawned += StartTrial;
    }


    public void StartTrial(GameObject ball)
    {
        if (IsTrialActive)
        {
            EndTrial(); // Finaliza o trial anterior se ainda estiver ativo
        }

        TrialCounter++;
        IsTrialActive = true;
        _currentBall = ball;

        // Adiciona um marcador no CSV
        trackingManager.RecordEvent($"TrialStart_{TrialCounter}");

        // Ativa a rede e o mesh da quadra após o trial 10
        if (TrialCounter > 50)
        {
            if (net != null) net.SetActive(true);
            if (courtMeshRenderer != null) courtMeshRenderer.enabled = true;
        }

        if (TrialCounter > 100)
        {
            if (zone != null) zone.SetActive(true);
        }


    }

    private void EndTrial()
    {
        if (!IsTrialActive) return;

        // Marca o fim do trial no CSV
        trackingManager.RecordEvent("TrialEnd");

        IsTrialActive = false;
        _currentBall = null;
    }

    private void OnDestroy()
    {
        if (ballSpawner != null)
        {
            ballSpawner.OnBallSpawned -= StartTrial;
        }
    }
}