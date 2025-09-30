using System.Collections.Generic;
using UnityEngine;

public class FollowPoints : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 bounds = new Vector3(30, 30, 30);
    private Vector3 targetPoint;
    private Vector3 direction;
    private Vector3 velocity;

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
        Vector3 newPoint;
        Vector3 forward = velocity == Vector3.zero ? Vector3.forward : velocity.normalized;
        do
        {
            newPoint = new Vector3(
                Random.Range(-bounds.x, bounds.x),
                Random.Range(-bounds.y, bounds.y),
                Random.Range(-bounds.z, bounds.z)
            );
        }
        while (Vector3.Dot((newPoint - transform.position).normalized, forward) < 0.3f);

        return newPoint;
    }
}
