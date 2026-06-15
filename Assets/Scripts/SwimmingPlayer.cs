using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class SwimmingPlayer : MonoBehaviour
{
    public float swimForce;
    public float speed;
    private Rigidbody2D body;
    public BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer;


    public enum State
    {
        Standard,
        HitStun,
        Drowning,
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
                break;
            case State.Drowning:
                Drowning();
                break;
        }
    }

    private void Movement()
    {
        boxCollider.enabled = true;
        deathTime = 0;

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
        state = State.Drowning;
    }

    private void Drowning()
    {
        anim.SetBool("drowning", true);
        anim.SetBool("swimming", false);
        transform.Rotate(Vector3.forward * (30 * Time.deltaTime));
        deathTime += Time.deltaTime;
        
        if (deathTime >= 3)
        {
            //Debug.Log("Respawn");
        }
    }
}
