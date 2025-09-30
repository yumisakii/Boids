using UnityEngine;

public class PhysicsForceModule : BoidModules
{
    private ForceCalculator forceCalculator;
    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);
        forceCalculator = new ForceCalculator();

        // Configurez vos param�tres ici
        forceCalculator.alpha1 = 5.0f;
        forceCalculator.alpha2 = 10.0f;
        forceCalculator.x0 = 2.0f;
        forceCalculator.k = 1.5f;
    }

    public override void Update()
    {
        base.Update();

        Vector3 totalForce = CalculateTotalForce();

        // Appliquer la force � la v�locit�
        _boid.velocity += totalForce * Time.deltaTime;
    }

    private Vector3 CalculateTotalForce()
    {
        Vector3 force = Vector3.zero;

        if (_neighborsList == null || _neighborsList.Count == 0)
            return force;

       
        foreach (Boid neighbor in _neighborsList)
        {
            // Vecteur de i vers j
            Vector3 direction = neighbor.transform.position - _boid.transform.position;
            float distance = direction.magnitude;

            if (distance < 0.01f) continue; // �viter division par z�ro

            // Normaliser la direction
            Vector3 normalizedDirection = direction / distance;

            // Calculer la magnitude de la force
            float forceMagnitude = forceCalculator.ForceExponential(distance);
            // OU : float forceMagnitude = forceCalculator.ForceInverseDistance(distance);

            // Ajouter la force : magnitude * direction
            force += forceMagnitude * normalizedDirection;
        }

        return force;
    }
}
