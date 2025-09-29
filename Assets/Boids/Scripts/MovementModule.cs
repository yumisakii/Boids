using UnityEngine;

public class MovementModule : BoidModules
{
    private Vector3 direction;
    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);
        direction = new Vector3(1, 0, 0);
    }

    public override void Update()
    {
        base.Update();
        _boid.transform.position += direction * 1f *  Time.deltaTime;
    }
}
