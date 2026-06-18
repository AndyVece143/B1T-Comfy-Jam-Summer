using UnityEngine;

public class Door : MonoBehaviour
{
    public bool open = false;
    public BoxCollider2D boxCollider;
    public Lever lever1;
    public Lever lever2;
    public Lever lever3;
    public Animator anim;
    public bool openingNoise = false;
    public AudioClip doorNoise;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lever1.active == true && lever2.active == true && lever3.active == true && openingNoise == false)
        {
            open = true;
            boxCollider.enabled = false;
            SoundManager.instance.PlaySound(doorNoise);
            openingNoise = true;
        }

        anim.SetBool("open", open);
    }
}
