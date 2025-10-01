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
        // On �vite les points dans les zones de r�pulsion
        const int maxAttempts = 30; // s�curit� pour ne pas boucler � l'infini
        int attempts = 0;

        while (attempts < maxAttempts)
        {
            Vector3 candidate = centerPosition + Random.insideUnitSphere * radius;
            bool insideRepulse = false;

            // V�rifie si le point est dans une zone de r�pulsion
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
                return candidate; // point valide trouv�
            }

            attempts++;
        }

        // Si pas trouv� apr�s X essais, on garde le dernier point (s�curit�)
        return centerPosition;
    }

}
