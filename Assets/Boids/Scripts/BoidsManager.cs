using System.Collections.Generic;
using UnityEngine;

public class BoidsManager : MonoBehaviour
{
    [SerializeField] private Boid boid;
    [SerializeField] private Boid leader;

    [SerializeField] private float distanceForNeighbors;
    private Vector3 leaderStartPosition;

    [SerializeField, Range(0,1)] private float cohesion;
    [SerializeField, Range(0,1)] private float separation;
    [SerializeField, Range(0,1)] private float alignement;
    [SerializeField, Range(0f, 5f)] private float followLeaderStrength = 1f;

    private float radiusZone = 75f;

    public Vector3 GetZoneCenter() => leaderStartPosition;
    public float GetZoneRadius() => radiusZone;


    private List<Boid> boids;

    private void Start()
    {
        boids = new List<Boid>();

        CreateLeader();

        for (int i = 0; i < 100; i++)
        {
            CreateBoid();
        }
    }

    private void CreateLeader()
    {
        leader = Instantiate(leader, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)), Quaternion.identity);
        leader.GetComponent<Renderer>().material.color = Color.red;
        leaderStartPosition = leader.transform.position;

        FollowPoints followPoints = leader.GetComponent<FollowPoints>();
        if (followPoints != null)
        {
            followPoints.Init(leaderStartPosition, 50f); // rayon du Gizmo
        }

    }

    private void CreateBoid()
    {
        Boid newBoid = Instantiate(boid, new Vector3(Random.Range(-10, 10), Random.Range(-10, 10), Random.Range(-10, 10)), Quaternion.identity);
        //Boid newBoid = Instantiate(boid);

        BoidData data = new BoidData
        {
            Cohesion = cohesion,
            Separation = separation,
            Alignement = alignement,
            FollowLeaderStrength = followLeaderStrength
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

    public Boid GetLeader()
    {
        return leader;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.5f);
        Gizmos.DrawSphere(leaderStartPosition, radiusZone);

        // Draw wire sphere outline.
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(leaderStartPosition, radiusZone);
    }

}
