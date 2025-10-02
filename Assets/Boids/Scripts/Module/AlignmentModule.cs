using System.Collections.Generic;
using UnityEngine;

public class AlignmentModule : BoidModules
{
    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);
    }

    public override void Update(BoidData data)
    {
        base.Update(data);

        _boid.velocity += GetAlignmentDirection() * _data.Alignement * Time.deltaTime;
    }

    private Vector3 GetAlignmentDirection()
    {
        if (_neighborsList == null || _neighborsList.Count == 0)
            return Vector3.zero;

        Vector3 averageVelocity = Vector3.zero;

        foreach (Boid neighbor in _neighborsList)
        {
            averageVelocity += neighbor.velocity;
        }

        averageVelocity /= _neighborsList.Count;

        return averageVelocity.normalized;
    }
}
