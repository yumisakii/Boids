using System.Collections.Generic;
using UnityEngine;

public class CohesionModule : BoidModules
{
    
    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);
    }

    public override void Update(BoidData data)
    {
        base.Update(data);

        _boid.velocity += GetBarycentre(_neighborsList) * _data.Cohesion * Time.deltaTime * 10;
    }

    private Vector3 GetBarycentre(List<Boid> neighbors)
    {
        Vector3 center = Vector3.zero;
        float weight = 0f;
        float totalWeight = 0f;

        if (neighbors.Count == 0)
        {
            return Vector3.zero;
        }

        foreach (Boid neighbor in neighbors)
        {
            float distance = Vector3.Distance(_boid.transform.position, neighbor.transform.position);
            weight = distance;

            center += neighbor.transform.position * weight;
            totalWeight += weight;
        }

        center /= totalWeight;

        Vector3 direction = (center - _boid.transform.position).normalized;
        return direction;
    }
}
