using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool bikini;
    public float speed;
    private Rigidbody2D body;
    public BoxCollider2D boxCollider;
    public Animator anim;
    public enum State
    {
        Standard,
        NoMove,
    }
    public State state;
    public SpriteRenderer inspectIcon;
    public SpriteRenderer talkIcon;
    public SpriteRenderer goIcon;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        inspectIcon.enabled = false;
        talkIcon.enabled = false;
        goIcon.enabled = false;
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
                break;
            case State.NoMove:
                break;
        }
        IconRotations();
        anim.SetBool("bikini", bikini);
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        body.linearVelocity = new Vector2(horizontalInput * speed, verticalInput * speed);

        //Flip Sprite
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }

        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetBool("move", horizontalInput != 0 || verticalInput != 0);
    }

    public void StopMoving(int react)
    {
        body.linearVelocity = new Vector2(0, 0);
        state = State.NoMove;
        anim.SetInteger("react", react);
    }

    public void StartMoving()
    {
        state = State.Standard;
        anim.SetInteger("react", 0);
    }

    private void IconRotations()
    {
        if (transform.localScale.x == 1)
        {
            inspectIcon.transform.localScale = Vector3.one;
            talkIcon.transform.localScale = Vector3.one;
            goIcon.transform.localScale = Vector3.one;
        }

        if (transform.localScale.x == -1)
        {
            inspectIcon.transform.localScale = new Vector3(-1, 1, 1);
            talkIcon.transform.localScale = new Vector3(-1, 1, 1);
            goIcon.transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "RoomTransition")
    //    {
    //        StartCoroutine(collision.gameObject.GetComponent<RoomTrigger>().BeginRoomTransition());
    //    }
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inspect" && state != State.NoMove)
        {
            inspectIcon.enabled = true;

            if (collision.gameObject.GetComponent<InteractableObject>().checker == false)
            {
                inspectIcon.color = Color.white;
            }
            else
            {
                inspectIcon.color = Color.gray;
            }
        }
        if (collision.gameObject.tag == "Inspect2" && state != State.NoMove)
        {
            inspectIcon.enabled = true;

            if (collision.gameObject.GetComponent<ChangingRoom>().checker == false)
            {
                inspectIcon.color = Color.white;
            }
            else
            {
                inspectIcon.color = Color.gray;
            }
        }
        if (collision.gameObject.tag == "LackOfBeef" && state != State.NoMove)
        {
            inspectIcon.enabled = true;

            if (collision.gameObject.GetComponent<LackOfBeef>().checker == false)
            {
                inspectIcon.color = Color.white;
            }
            else
            {
                inspectIcon.color = Color.gray;
            }
        }


        if (collision.gameObject.tag == "Sandwich" && state != State.NoMove)
        {
            talkIcon.enabled = true;
            if (collision.gameObject.GetComponent<SandwichShop>().checker == false)
            {
                talkIcon.color = Color.white;
            }
            else
            {
                talkIcon.color = Color.gray;
            }
        }
        if (collision.gameObject.tag == "Bro" && state != State.NoMove)
        {
            talkIcon.enabled = true;
            if (collision.gameObject.GetComponent<BroTalking>().checker == false)
            {
                talkIcon.color = Color.white;
            }
            else
            {
                talkIcon.color = Color.gray;
            }
        }

        if (collision.gameObject.tag == "NPC" && state != State.NoMove)
        {
            talkIcon.enabled = true;
            if (collision.gameObject.GetComponent<NPC>().checker == false)
            {
                talkIcon.color = Color.white;
            }
            else
            {
                talkIcon.color = Color.gray;
            }
        }
        if (collision.gameObject.tag == "Bartender2" && state != State.NoMove)
        {
            talkIcon.enabled = true;
            if (collision.gameObject.GetComponent<Bartender2>().checker == false)
            {
                talkIcon.color = Color.white;
            }
            else
            {
                talkIcon.color = Color.gray;
            }
        }
        if (collision.gameObject.tag == "RoomTransition" && state != State.NoMove)
        {
            goIcon.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Inspect" || collision.gameObject.tag == "Inspect2" ||collision.gameObject.tag == "LackOfBeef")
        {
            inspectIcon.enabled = false;
        }
        if (collision.gameObject.tag == "Sandwich" || (collision.gameObject.tag == "Bro") || (collision.gameObject.tag == "NPC") || (collision.gameObject.tag == "Bartender2"))
        {
            talkIcon.enabled = false;
        }

        if (collision.gameObject.tag == "RoomTransition")
        {
            goIcon.enabled = false;
        }
    }
}
