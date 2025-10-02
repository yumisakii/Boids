using System.Collections.Generic;
using UnityEngine;

public class SeparationModule : BoidModules
{

    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);
    }

    public override void Update(BoidData data)
    {
        base.Update(data);

        _boid.velocity += GetSeparationDirection(_closeNeighborsList) * _data.Separation * Time.deltaTime * 10;
    }

    private Vector3 GetSeparationDirection(List<Boid> neighbors)
    {
        Vector3 separation = Vector3.zero;

        if (neighbors == null || neighbors.Count == 0)
        {
            return Vector3.zero;
        }

        foreach (Boid neighbor in neighbors)
        {
            Vector3 diff = _boid.transform.position - neighbor.transform.position;
            float distance = diff.magnitude;

            if (distance < 0.01f) continue;

            float separationForce = (1f - Mathf.Clamp01(distance / 10f)) * 10f;

            Vector3 direction = diff / distance;
            separation += direction * separationForce;
        }

        return separation.normalized;
    }
}
