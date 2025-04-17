using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrajectoryTrackerVR : MonoBehaviour
{
    // Lista para armazenar trajetórias
    private List<float[]> Trajectory;

    private void Start()
    {
        Trajectory = new List<float[]>();

    }

    void FixedUpdate()
    {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        float[] entry = new float[] {
            Time.timeSinceLevelLoad,  // Tempo desde o início
            position.x, position.y, position.z,  // Posição XYZ
            rotation.x, rotation.y, rotation.z, rotation.w  // Rotação (quaternion)
        };

        Trajectory.Add(entry);
    }

    public void Clear()
    {
        Trajectory.Clear();
    }
}
