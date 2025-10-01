using UnityEngine;

public class SpecialBoidModule : BoidModules
{
    private bool isColibris = false;
    private float impulseTimer = 0.25f;
    private float timer = 0;
    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);

        int n = Random.Range(0, 4);
        //int n = 3;
        switch (n)
        {
            case 0: // CoLlant
                _data.Cohesion = 1.8f;
                _data.Separation = 0.6f;
                _boid.SetColor(Color.green);
                break;

            case 1: // Retardataire
                _boid.speedMultiplicator = 0.8f;
                _boid.SetColor(Color.blue);
                break;

            case 2: // Entousiate
                _boid.speedMultiplicator = 2f;
                _data.Cohesion = 2;
                _boid.SetColor(Color.yellow);
                break;

            case 3: // COLIBRIS
                _data.Cohesion = 1.25f;
                isColibris = true;
                _boid.SetColor(Color.red);
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if (isColibris)
        {
            if (timer > impulseTimer * 2)
            {
                timer = 0;
            }
            else if (timer > impulseTimer)
            {
                _boid.speedMultiplicator = 100;
                timer++;
            }
            else
            {
                _boid.speedMultiplicator = 0;
                timer += Time.deltaTime;
            }
        }

    }
}
