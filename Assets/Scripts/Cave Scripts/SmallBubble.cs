using UnityEngine;

public class SmallBubble : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed;
    public float timer;
    private float stateTimer;
    public float stateTimerMax;
    public int state = 0;
    public Animator anim;
    public float amplitude;
    public float frequency;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        stateTimer = stateTimerMax;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float xSpeed = amplitude * frequency * Mathf.Cos(Time.time * frequency);
        body.linearVelocity = new Vector2(xSpeed, speed);
        timer -= Time.deltaTime;

        stateTimer -= Time.deltaTime;
        if (stateTimer < 0 && state < 2)
        {
            state++;
            anim.SetInteger("state", state);
            stateTimer = stateTimerMax;
        }
        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
