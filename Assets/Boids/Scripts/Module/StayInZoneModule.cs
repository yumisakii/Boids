using UnityEngine;

public class StayInZoneModule : BoidModules
{
    private Vector3 center;
    private float radius;

    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);
        center = _boid.GetBoidsManager().GetZoneCenter();
        radius = _boid.GetBoidsManager().GetZoneRadius();
    }

    public override void Update(BoidData data)
    {
        base.Update(data);

        Vector3 toCenter = center - _boid.transform.position;
        float distance = toCenter.magnitude;

        if (distance > radius * 0.9f) // commence à pousser avant la sortie
        {
            float strength = Mathf.InverseLerp(radius, radius * 0.9f, distance); // Valeur de 0 à 1
            Vector3 correction = 80f * strength * Time.deltaTime * toCenter.normalized;
            _boid.velocity += correction;
        }
    }
}
