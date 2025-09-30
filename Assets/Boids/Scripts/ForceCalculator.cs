using UnityEngine;

public class ForceCalculator
{
    // Paramètres pour la fonction de force
    public float alpha1 = 1.0f;  // Force d'attraction
    public float alpha2 = 2.0f;  // Force de répulsion
    public float x0 = 2.0f;      // Distance d'équilibre
    public float k = 1.0f;       // Coefficient pour gaussienne

    // Calcule f(x) - Version inverse de distance
    public float ForceInverseDistance(float distance)
    {
        if (distance < 0.01f) distance = 0.01f; // Éviter division par zéro

        float attraction = alpha1 / (distance * distance);
        float repulsion = alpha2 / (distance * distance * distance * distance);

        return attraction - repulsion;
    }

    // Calcule f(x) - Version exponentielle/gaussienne
    public float ForceExponential(float distance)
    {
        if (distance < 0.01f) distance = 0.01f;

        float ratio = (distance - x0) / x0;
        float attraction = alpha1 * Mathf.Exp(-k * ratio * ratio);
        float repulsion = alpha2 * Mathf.Exp(distance / x0);

        return attraction - repulsion;
    }
}
