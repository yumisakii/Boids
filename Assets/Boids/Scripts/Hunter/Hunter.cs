using UnityEngine;

public class Hunter : MonoBehaviour
{
    private BoidsManager manager;
    private Vector3 velocity;
    private Vector3 destination;
    private float speed;
    private float timer;
    
    void Start()
    {
        velocity = Vector3.zero;
        speed = 2f;
    }

    void Update()
    {
        if (timer == 0)
        {
            destination = manager.GetBoidsMiddle();
            velocity = (destination - transform.position).normalized;
            timer += Time.deltaTime;
        }
        else if (timer < 1)
        {
            transform.position += velocity * speed;
            timer += Time.deltaTime;
        }
        else if(timer < 3) { timer += Time.deltaTime; }

        else
        {
            timer = 0;
        }
    }

    public void Init()
    {

    }

    public void SetManager(BoidsManager manager)
    {
        this.manager = manager;
    }
}
