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

    private GameObject currentBall;

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
        currentBall = ball;

        // Adiciona um marcador no CSV
        trackingManager.RecordEvent($"TrialStart_{TrialCounter}");

        // Configurar listeners para a bola
        var tennisBall = ball.GetComponent<TennisBall>();
        if (tennisBall == null)
        {
            tennisBall = ball.AddComponent<TennisBall>();
        }

        tennisBall.OnRacketImpact += RegisterImpact;
    }

    public void RegisterImpact(Vector3 impactPosition)
    {
        if (!IsTrialActive) return;

        // Marca o impacto no CSV 
        trackingManager.RecordEvent("RacketImpact");
    }


    private void EndTrial()
    {
        if (!IsTrialActive) return;

        // Marca o fim do trial no CSV
        trackingManager.RecordEvent("TrialEnd");

        // Limpeza
        if (currentBall != null)
        {
            var tennisBall = currentBall.GetComponent<TennisBall>();
            if (tennisBall != null)
            {
                tennisBall.OnRacketImpact -= RegisterImpact;
            }
        }

        IsTrialActive = false;
        currentBall = null;
    }

    private void OnDestroy()
    {
        if (ballSpawner != null)
        {
            ballSpawner.OnBallSpawned -= StartTrial;
        }
    }
}