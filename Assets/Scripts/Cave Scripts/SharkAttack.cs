using UnityEngine;

public class SharkAttack : MonoBehaviour
{
    public Shark shark;
    public BoxCollider2D boxCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            shark.attack = true;
        }
    }
}
