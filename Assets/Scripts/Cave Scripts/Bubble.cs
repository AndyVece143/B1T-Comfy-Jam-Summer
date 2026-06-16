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
            boxCollider.enabled = true;
            body.linearVelocity = new Vector2(0, speed);
            anim.SetInteger("state", state);
            timer -= Time.deltaTime;
            if (timer < 0 && breaking == false)
            {
                breaking = true;
                StartCoroutine(BreakBubble());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Breathing")
        {
            player.BeginBreathing();
            StartCoroutine(BreakBubble());
        }
    }

    IEnumerator BreakBubble()
    {
        anim.SetTrigger("break");
        yield return new WaitForSeconds(0.666f);
        Destroy(gameObject);
    }
}
