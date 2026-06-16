using UnityEngine;

public class Orb : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D body;
    public bool goingRight;
    private bool goingDown;
    public float speed;
    public float amplitude;
    public float frequency;
    public float startingBoost;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        StartingVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (goingDown == false && body.linearVelocity.y < 0)
        {
            goingDown = true;
        }

        if (goingDown)
        {
            float xSpeed = amplitude * frequency * Mathf.Cos(Time.time * frequency);
            body.linearVelocity = new Vector2(xSpeed, -speed);
        }
    }

    void StartingVelocity()
    {
        if (goingRight)
        {
            body.linearVelocity = new Vector2(2, startingBoost);
        }

        else
        {
            body.linearVelocity = new Vector2(-2, startingBoost);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
