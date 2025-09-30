using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private BoidsManager boidsManager;

    private List<BoidModules> modules;

    public Vector3 velocity = Vector3.zero;

    public void Init(BoidData data)
    {
        modules = new List<BoidModules>();

        //AddModule<PhysicsForceModule>(data);

        AddModule<CohesionModule>(data);
        AddModule<SeparationModule>(data);
        AddModule<AlignmentModule>(data);
        AddModule<MovementModule>(data);

        for (int i = 0; i < modules.Count; i++)
        {
            modules[i].Init(this, data);
        }
    }

    void Update()
    {
        for (int i = 0; i < modules.Count; i++)
        {
            modules[i].Update();
        }
    }

    public void SetBoidsManager(BoidsManager boidsManager)
    {
        this.boidsManager = boidsManager;
    }
     public BoidsManager GetBoidsManager()
    {
        return this.boidsManager;
    }

    private T AddModule<T>(BoidData data) where T : BoidModules, new()
    {
        T module = new T();
        module.Init(this, data);
        modules.Add(module);
        return module;
    }
}
