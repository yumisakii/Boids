using System.Collections.Generic;
using UnityEngine;

public class RepulseModule : MonoBehaviour
{
    [SerializeField] private float maxForce;
    [SerializeField] private float radius;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void ApplyRepulse(Boid _boid)
    {
        Vector3 vector = _boid.transform.position - transform.position;
        float vecLength = vector.magnitude;
        float f = maxForce * (1f - (vecLength / radius));

        _boid.Repulse(vector.normalized, f);
    }

    public float GetRadius()
    {
        return radius;
    }

}
