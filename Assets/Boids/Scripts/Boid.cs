using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private BoidsManager boidsManager;
    private List<BoidModules> modules;

    public Vector3 velocity = Vector3.zero;
    public float speedMultiplicator;

    public void Init(BoidData data)
    {
        speedMultiplicator = 1;
        int n = Random.Range(0, 10);// Chance to be a spacial Boid

        modules = new List<BoidModules>();

        AddModule<CohesionModule>(data);
        AddModule<SeparationModule>(data);
        AddModule<AlignmentModule>(data);
        AddModule<FollowLeaderModule>(data);
        AddModule<StayInZoneModule>(data);
        AddModule<MovementModule>(data);

        if (n == 0) {
            AddModule<SpecialBoidModule>(data);


        }

        for (int i = 0; i < modules.Count; i++)
        {
            modules[i].Init(this, data);
        }
    }

    public void Repulse(Vector3 direction, float force)
    {
        transform.position += force * Time.deltaTime * direction;
    }

    private void Update()
    {
        if (modules == null) return;

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

    public void SetColor(Color color)
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = color;
    }

    private T AddModule<T>(BoidData data) where T : BoidModules, new()
    {
        T module = new T();
        module.Init(this, data);
        modules.Add(module);
        return module;
    }

    
}
