using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// ALTERAR PARA A APROACH COM O SWITCH
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


    private void Start()
    {
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