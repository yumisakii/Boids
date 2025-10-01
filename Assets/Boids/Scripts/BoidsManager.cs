using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    [SerializeField] private Boid boid;

    [SerializeField] private float distanceForNeighbors;

    [SerializeField, Range(0,1)] private float cohesion;
    [SerializeField, Range(0,1)] private float separation;
    [SerializeField, Range(0,1)] private float alignement;
    
    private List<Boid> boids;

    void Start()
    {
        boids = new List<Boid>();

        for (int i = 0; i < 100; i++)
        {
            CreateBoid();
        }
    }

    void Update()
    {
        
    }

    void CreateBoid()
    {
        Boid newBoid = Instantiate(boid, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)), Quaternion.identity);

        BoidData data = new BoidData
        {
            Cohesion = cohesion,
            Separation = separation,
            Alignement = alignement
        };

        newBoid.SetBoidsManager(this);
        newBoid.Init(data);
        boids.Add(newBoid);
    }

    public (List<Boid>, List<Boid>) GetNeighborsBoids(Boid boid)
    {
        List<Boid> closeNeighbors = new List<Boid>();
        List<Boid> neighbors = new List<Boid>();

        float distanceForCloseNeighbors = distanceForNeighbors - (distanceForNeighbors / 3);

        for (int i = 0; i < boids.Count; i++)
        {
            if (boids[i] == boid) continue;

            float distance = Vector3.Distance(boid.transform.position, boids[i].transform.position);

            if (distance < distanceForNeighbors)
            {
                if (distance < distanceForCloseNeighbors)
                {
                    closeNeighbors.Add(boids[i]);
                }
                neighbors.Add(boids[i]);
            }
        }
        return (closeNeighbors, neighbors);
    }
}
