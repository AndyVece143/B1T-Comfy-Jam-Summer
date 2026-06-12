using UnityEngine;

public class BigDialogueTrigger : MonoBehaviour
{
    public Player player;
    public BoxCollider2D boxCollider;
    public BigDialogue bigDialogue;
    bool triggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered == false && collision.gameObject == player.gameObject)
        {
            triggered = true;
            player.StopMoving(1);
            BigDialogue newBigDialogue = Instantiate(bigDialogue);
        }
    }
}
