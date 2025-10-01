using System.Collections.Generic;
using UnityEngine;

public abstract class BoidModules
{
    public Boid _boid;
    public BoidsManager boidsManager;
    public BoidData _data;

    protected List<Boid> _closeNeighborsList;
    protected List<Boid> _neighborsList;
    
    

    public virtual void Init(Boid boid, BoidData data)
    {
        _boid = boid;
        _data = data;
        boidsManager = boid.GetBoidsManager();
    }
   
    public virtual void Update()
    {
        var (closeNeighbors, neighbors) = boidsManager.GetNeighborsBoids(_boid);
        _closeNeighborsList = closeNeighbors;
        _neighborsList = neighbors;
    }
}
