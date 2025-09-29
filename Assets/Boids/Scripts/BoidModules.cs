using System.Collections.Generic;
using UnityEngine;

public class BoidModules
{
    public Boid _boid;
    public BoidsManager boidsManager;
    public BoidData _data;
    public List<Vector3> _neighborsPosList;


    public virtual void Init(Boid boid, BoidData data)
    {
        _boid = boid;
        _data = data;
        boidsManager = boid.GetBoidsManager();
    }
   
    public virtual void Update()
    {
        _neighborsPosList = boidsManager.GetNeighborsBoids(_boid);
    }
}
