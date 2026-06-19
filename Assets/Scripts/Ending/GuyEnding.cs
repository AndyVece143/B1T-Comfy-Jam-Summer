using UnityEngine;

public class GuyEnding : MonoBehaviour
{
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void GetScared()
    {
        anim.SetTrigger("scared");
    }
}
