using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow;

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

        CreateBoid();
        CreateBoid();
        CreateBoid();
    }

    void Update()
    {
        
    }

    void CreateBoid()
    {
        Boid newBoid = Instantiate(boid, new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2)), Quaternion.identity);
        //Boid newBoid = Instantiate(boid);

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

    public List<Vector3> GetNeighborsBoids(Boid boid)
    {
        List<Vector3> neighbors = new List<Vector3>();

        for (int i = 0; i < boids.Count; i++)
        {
            if (Vector3.Distance(boid.transform.position, boids[i].transform.position) < distanceForNeighbors)
            {
                neighbors.Add(boids[i].transform.position);
            }
        }

        return neighbors;
    }
}
