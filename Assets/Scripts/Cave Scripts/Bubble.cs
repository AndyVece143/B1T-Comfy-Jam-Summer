using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public int state = 0;
    public Animator anim;
    public BoxCollider2D boxCollider;
    public Rigidbody2D body;
    public float timer;
    public float stateTimer;
    public float stateTimerMax;
    private bool breaking;
    public float speed;
    public SwimmingPlayer player;

    public float amplitude;
    public float frequency;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        body = GetComponent<Rigidbody2D>();
        stateTimer = stateTimerMax;
        boxCollider.enabled = false;
        breaking = false;
        player = SwimmingPlayer.FindAnyObjectByType<SwimmingPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state < 3)
        {
            stateTimer -= Time.deltaTime;
            if (stateTimer < 0)
            {
                state++;
                stateTimer = stateTimerMax;
                anim.SetInteger("state", state);
            }
        }

        else
        {
            if (breaking == false)
            {
                boxCollider.enabled = true;
                float xSpeed = amplitude * frequency * Mathf.Cos(Time.time * frequency);
                body.linearVelocity = new Vector2(xSpeed, speed);
                anim.SetInteger("state", state);
                timer -= Time.deltaTime;

                if (timer < 0 && breaking == false)
                {
                    breaking = true;
                    body.linearVelocity = new Vector2(0, 0);
                    StartCoroutine(BreakBubble());
                }
            }

            else
            {
                body.linearVelocity = new Vector2(0, 0);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Breathing")
        {
            breaking = true;
            player.BeginBreathing();
            StartCoroutine(BreakBubble());
        }
    }

    IEnumerator BreakBubble()
    {
        //body.bodyType = RigidbodyType2D.Static;
        body.linearVelocity = new Vector2(0, 0);
        anim.SetTrigger("break");
        yield return new WaitForSeconds(0.666f);
        Destroy(gameObject);
    }
}
