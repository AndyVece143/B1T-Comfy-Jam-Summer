using UnityEngine;

public class SwimmingPlayer : MonoBehaviour
{
    public float swimForce;
    public float speed;
    private Rigidbody2D body;
    public BoxCollider2D boxCollider;
    public BoxCollider2D breathingSpot;

    [SerializeField] private LayerMask groundLayer;

    public enum State
    {
        Standard,
        HitStun,
        Drowning,
        Breathing,
    }
    public State state;
    public Animator anim;
    public float swimCooldown;
    public float swimTimer;
    public float air;
    public CameraController mainCamera;
    private float deathTime;
    public AudioClip swim;
    public AudioClip drowning;
    public AudioClip breathing;
    private float breathingTimer;
    public float breathingTimerMax;

    private float hitStunTime;
    private float iFrameTimer;
    public bool iFrames;
    [SerializeField] private AudioClip hurtSound;
    

    public Transform mouth;
    public float bubbleTimer;
    public SmallBubble smallBubble;
    public SmallerBubble smallerBubble;
    public float smallerBubbleTimer;
    public GameManager manager;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Standard:
                Movement();
                HoldingBreath();
                break;
            case State.HitStun:
                HitStun();
                break;
            case State.Drowning:
                Drowning();
                break;
            case State.Breathing:
                Breathing();
                break;
        }
    }

    private void Movement()
    {
        anim.SetBool("breathing", false);
        anim.SetBool("drowning", false);
        breathingTimer = breathingTimerMax;
        boxCollider.enabled = true;
        deathTime = 0;
        hitStunTime = 0;
        transform.rotation = new Quaternion(0, 0, 0, 0);

        float horizontalInput = Input.GetAxis("Horizontal");

        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        swimTimer -= Time.deltaTime;

        //Swimming code
        if (swimTimer <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SoundManager.instance.PlaySound(swim);
                SwimForceMethod();
                swimTimer = swimCooldown;
            }
        }

        iFrameTimer -= Time.deltaTime;
        if (iFrameTimer < 0)
        {
            iFrames = false;
        }

        if (iFrames)
        {
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = Color.white;
            Physics2D.IgnoreLayerCollision(6, 7, false);
        }

        smallerBubbleTimer -= Time.deltaTime;
        if (smallerBubbleTimer < 0)
        {
            SmallerBubble newSmallerBubble = Instantiate(smallerBubble);
            newSmallerBubble.transform.position = mouth.position;
            smallerBubbleTimer = 1.4f;
        }

        //Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Animations
        anim.SetBool("move", horizontalInput != 0);
        anim.SetBool("grounded", IsGrounded());
        anim.SetBool("swimming", AmISwimming());
        anim.SetBool("danger", AmIDying());
    }

    private void HitStun()
    {
        anim.SetBool("hurt", true);
        anim.SetBool("swimming", false);
        if (IsFacingRight())
        {
            body.linearVelocity = new Vector2(-4f, 0f);
        }
        else
        {
            body.linearVelocity = new Vector2(4f, 0f);
        }
        transform.Rotate(Vector3.forward * 360 * Time.deltaTime);
        hitStunTime += Time.deltaTime;
        if (hitStunTime > 1)
        {
            anim.SetBool("hurt", false);
            state = State.Standard;
            iFrameTimer = 3;
        }
    }

    private void KnockBack()
    {
        air -= 5;
        anim.SetBool("swimming", false);
        SoundManager.instance.PlaySound(hurtSound);
        Physics2D.IgnoreLayerCollision(6, 7);
        state = State.HitStun;
        iFrames = true;
    }

    private void SwimForceMethod()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, swimForce);
    }

    private bool AmISwimming()
    {
        if (swimTimer > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool IsFacingRight()
    {
        if (transform.localScale.x == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void HoldingBreath()
    {
        air -= Time.deltaTime;

        if (air <= 0)
        {
            BenDrowned();
        }
    }

    private bool AmIDying()
    {
        if (air <= 15)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void BenDrowned()
    {
        Debug.Log("Ben Drowned");
        SoundManager.instance.PlaySound(drowning);
        mainCamera.state = CameraController.State.StayStill;
        boxCollider.enabled = false;
        body.linearVelocity = new Vector2(0, 0);
        bubbleTimer = 0;
        state = State.Drowning;
    }

    private void Drowning()
    {
        anim.SetBool("drowning", true);
        anim.SetBool("swimming", false);
        anim.SetBool("hurt", false);
        transform.Rotate(Vector3.forward * (30 * Time.deltaTime));
        deathTime += Time.deltaTime;
        
        if (deathTime >= 3)
        {
            //Debug.Log("Respawn");
            StartCoroutine(manager.RespawnPlayer());
        }

        bubbleTimer += Time.deltaTime;
        if (bubbleTimer >= 0.2f && deathTime <= 2)
        {
            SmallBubble newSmallBubble = Instantiate(smallBubble);
            newSmallBubble.transform.position = mouth.position;
            bubbleTimer = 0;
        }
    }

    public void BeginBreathing()
    {
        state = State.Breathing;
        air = 45;
        body.linearVelocity = new Vector2(0, 0);
        SoundManager.instance.PlaySound(breathing);
    }

    private void Breathing()
    {
        anim.SetBool("breathing", true);
        anim.SetBool("swimming", false);

        breathingTimer -= Time.deltaTime;

        if (breathingTimer < 0)
        {
            state = State.Standard;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (iFrames == false && state != State.HitStun)
        {
            if (collision.collider.tag == "Enemy")
            {
                KnockBack();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shark")
        {
            BenDrowned();
        }
    }
}
