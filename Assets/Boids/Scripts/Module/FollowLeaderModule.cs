using UnityEngine;

public class FollowLeaderModule : BoidModules
{
    private Boid _leader;
    private float _followStrength = 1.0f;

    public override void Init(Boid boid, BoidData data)
    {
        base.Init(boid, data);

        _leader = _boid.GetBoidsManager()?.GetLeader();
        _followStrength = data.FollowLeaderStrength;
    }

    public override void Update()
    {
        base.Update();

        if (_leader == null || _leader == _boid)
            return;

        Vector3 directionToLeader = (_leader.transform.position - _boid.transform.position).normalized;
        _boid.velocity += directionToLeader * _followStrength * Time.deltaTime * 10;
    }
}
