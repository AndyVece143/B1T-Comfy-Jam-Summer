using UnityEngine;

public class Fish : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Rigidbody2D body;
    public float speed;
    public Transform ledgeDetector;
    public float wallDistance;
    public LayerMask groundLayer;
    private bool facingRight = true;
    private Vector2 forwards;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        body.linearVelocity = new Vector2(speed, body.linearVelocity.y);
        if (facingRight)
        {
            forwards = Vector2.right;
        }
        else
        {
            forwards = Vector2.left;
        }

        RaycastHit2D hitWall = Physics2D.Raycast(ledgeDetector.position, forwards, wallDistance, groundLayer);

        if (hitWall == true)
        {
            Rotate();
        }
    }

    void Rotate()
    {
        transform.Rotate(0, 180, 0);
        speed = -speed;

        if (facingRight)
        {
            facingRight = false;
        }
        else
        {
            facingRight = true;
        }
    }
}
