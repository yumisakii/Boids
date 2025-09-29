using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private BoidsManager boidsManager;
    private BoidData boidData;

    private List<BoidModules> modules;
    public void Init(BoidData data)
    {
        modules = new List<BoidModules>();
        boidData = data;

        AddModule<MovementModule>(data);
        AddModule<CohesionModule>(data);

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
