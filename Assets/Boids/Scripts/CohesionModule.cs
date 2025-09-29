using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class CohesionModule : BoidModules
{
    
    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);
    }

    public override void Update()
    {
        base.Update();

        _boid.transform.position += GetBarycentre(_neighborsPosList) * _data.Cohesion * Time.deltaTime;
    }

    private Vector3 GetBarycentre(List<Vector3> neighbors)
    {
        Vector3 center = Vector3.zero;

        if (neighbors.Count == 0)
        {
            return Vector3.zero;
        }

        foreach (Vector3 neighbor in neighbors)
        {
            center += neighbor;
        }

        center = center/neighbors.Count;

        Debug.Log(center);

        Vector3 direction = (center - _boid.transform.position).normalized;
        return direction;
    }
}
