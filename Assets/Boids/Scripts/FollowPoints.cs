using System.Collections.Generic;
using UnityEngine;

public class FollowPoints : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 targetPoint;
    private Vector3 direction;
    private Vector3 velocity;
    private Vector3 centerPosition;
    private float radius;

    public void Init(Vector3 center, float radius)
    {
        centerPosition = center;
        this.radius = radius;
    }

    private void Start()
    {
        // Premier point au hasard
        targetPoint = GetRandomPoint();
        velocity = (targetPoint - transform.position).normalized * speed;
    }

    private void Update()
    {
        direction = targetPoint - transform.position;

        // Lissage de la direction -> mouvement plus fluide
        velocity = Vector3.Lerp(velocity, direction.normalized * speed, Time.deltaTime * 2f);
        transform.position += velocity * Time.deltaTime;

        // Quand assez proche, on choisit un nouveau point
        if (direction.magnitude < 1f)
        {
            targetPoint = GetRandomPoint();
        }
    }

    private Vector3 GetRandomPoint()
    {
        return centerPosition + Random.insideUnitSphere * radius;
    }

}
