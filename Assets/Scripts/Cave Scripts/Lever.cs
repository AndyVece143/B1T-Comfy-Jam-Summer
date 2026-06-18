using UnityEngine;

public class Lever : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public bool active = false;
    public Animator anim;
    public AudioClip click;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("active", active);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (active == false)
        {
            active = true;
            SoundManager.instance.PlaySound(click);
        }
    }
}
