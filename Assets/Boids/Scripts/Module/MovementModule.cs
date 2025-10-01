using UnityEngine;

public class MovementModule : BoidModules
{
    private float baseSpeed = 10f;
    private float maxSpeed = 30f;
    private float friction = 0.98f;
    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);
        _boid.velocity = new Vector3(
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f),
            Random.Range(-1f, 1f)
            ).normalized * baseSpeed;
    }

    public override void Update()
    {
        base.Update();

        _boid.velocity = _boid.velocity.normalized * baseSpeed * _boid.speedMultiplicator;

        if (_boid.velocity.magnitude > maxSpeed)
        {
            Debug.Log("Max speed cap");
            _boid.velocity = _boid.velocity.normalized * maxSpeed;
        }

        _boid.transform.position += _boid.velocity *  Time.deltaTime;

        _boid.velocity *= friction;
    }
}
