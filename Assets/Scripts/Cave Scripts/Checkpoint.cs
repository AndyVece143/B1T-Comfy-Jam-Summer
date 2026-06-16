using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private GameManager manager;
    private SwimmingPlayer player;
    private bool active = false;
    public Animator anim;
    public AudioClip sound;
    private BoxCollider2D boxCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = SwimmingPlayer.FindAnyObjectByType<SwimmingPlayer>();
        manager = GameManager.FindAnyObjectByType<GameManager>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("active", active);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && active == false)
        {
            SoundManager.instance.PlaySound(sound);
            active = true;
            manager.activeCheckpoint = this;
        }
    }
}
