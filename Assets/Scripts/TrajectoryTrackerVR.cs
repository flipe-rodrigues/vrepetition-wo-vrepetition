using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrajectoryTrackerVR : MonoBehaviour
{
    // Lista para armazenar trajet�rias
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
            Time.timeSinceLevelLoad,  // Tempo desde o in�cio
            position.x, position.y, position.z,  // Posi��o XYZ
            rotation.x, rotation.y, rotation.z, rotation.w  // Rota��o (quaternion)
        };

        Trajectory.Add(entry);
    }

    public void Clear()
    {
        Trajectory.Clear();
    }
}
