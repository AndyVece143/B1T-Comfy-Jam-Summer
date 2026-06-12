using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public Animator anim;

    public void BecomeTrans()
    {
        anim.Play("trans");
    }
}
