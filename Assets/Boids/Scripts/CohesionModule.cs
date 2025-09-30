using System.Collections.Generic;
using UnityEngine;

public class CohesionModule : BoidModules
{
    
    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);
    }

    public override void Update()
    {
        base.Update();

        //_boid.transform.position += GetBarycentre(_neighborsList) * _data.Cohesion * Time.deltaTime * 10;
        _boid.velocity += GetBarycentre(_neighborsList) * _data.Cohesion * Time.deltaTime * 10;
    }

    private Vector3 GetBarycentre(List<Boid> neighbors)
    {
        Vector3 center = Vector3.zero;

        if (neighbors.Count == 0)
        {
            return Vector3.zero;
        }

        foreach (Boid neighbor in neighbors)
        {
            center += neighbor.transform.position;
        }

        center = center/neighbors.Count;

        Vector3 direction = (center - _boid.transform.position).normalized;
        return direction;
    }
}
