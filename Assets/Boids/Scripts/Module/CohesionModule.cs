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
            if (Vector3.Distance(_boid.transform.position, neighbor.transform.position) > 10)
            {
                center += neighbor.transform.position * 1.5f;
            }
            else // Si le voisin n'est pas trop eloigner 
            {
                center += neighbor.transform.position;
            }
            
        }

        center = center/neighbors.Count;

        Vector3 direction = (center - _boid.transform.position).normalized;
        return direction;
    }
}
