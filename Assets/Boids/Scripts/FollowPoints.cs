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
    private RepulseModule[] repulseObstacles;

    public void Init(Vector3 center, float radius, RepulseModule[] repulses)
    {
        centerPosition = center;
        this.radius = radius;
        this.repulseObstacles = repulses;
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
        // On évite les points dans les zones de répulsion
        const int maxAttempts = 30; // sécurité pour ne pas boucler à l'infini
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 candidate = centerPosition + Random.insideUnitSphere * radius;
            bool insideRepulse = false;

            // Vérifie si le point est dans une zone de répulsion
            foreach (var repulse in repulseObstacles)
            {
                float dist = Vector3.Distance(candidate, repulse.transform.position);
                if (dist < repulse.GetRadius())
                {
                    insideRepulse = true;
                    break;
                }
            }

            if (!insideRepulse)
            {
                return candidate; // point valide trouvé
            }

            attempts++;
        }

        // Si pas trouvé après X essais, on garde le dernier point (sécurité)
        return centerPosition;
    }

}
