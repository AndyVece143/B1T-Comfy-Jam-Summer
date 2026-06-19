using UnityEngine;

public class PotOfGold : MonoBehaviour
{
    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("big", StaticData.beganAtBeginning);
    }
}
