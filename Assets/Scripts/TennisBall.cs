using System;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    public event Action<Vector3> OnRacketImpact;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Racket"))
        {
            OnRacketImpact?.Invoke(collision.contacts[0].point);
        }
    }

}
