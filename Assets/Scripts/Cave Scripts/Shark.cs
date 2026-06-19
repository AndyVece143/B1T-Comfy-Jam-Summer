using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Shark : MonoBehaviour
{
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D body;
    public float speed;
    public bool attack = false;
    public Animator anim;
    public Vector2 originalPosition;
    public SwimmingPlayer player;
    private float swimSpeed;
    public Light2D spookyLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (attack == true)
        {
            spookyLight.enabled = true;
            swimSpeed = speed;
            if (PlayerDistance() > 15)
            {
                swimSpeed = speed * 2;
            }
            if (PlayerDistance() < 12)
            {
                swimSpeed = speed / 2;
            }
            //Debug.Log(swimSpeed);
            body.linearVelocity = new Vector2(swimSpeed, 0);
        }
        else
        {
            spookyLight.enabled = false;
            body.linearVelocity = new Vector2(0, 0);
        }

        anim.SetBool("attack", attack);
    }

    float PlayerDistance()
    {
        float distance = player.transform.position.x - transform.position.x;
        Debug.Log(distance);
        return distance;
    }

    public void Respawn()
    {
        attack = false;
        transform.position = originalPosition;
    }
}
