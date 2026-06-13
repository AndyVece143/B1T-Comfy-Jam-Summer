using UnityEngine;

public class BroDialogueTrigger : MonoBehaviour
{
    public Player player;
    public BoxCollider2D boxCollider;
    private bool triggered = false;
    public BeachManager manager;
    public BigDialogue dialogue1;
    public BigDialogue dialogue2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject && triggered == false)
        {
            switch (manager.progress)
            {
                case 0:
                    player.StopMoving(1);
                    BigDialogue newBigDialogue = Instantiate(dialogue1);
                    manager.UpdateProgress();
                    break;
                case 1:
                    break;
                case 2:
                    triggered = true;
                    player.StopMoving(1);
                    BigDialogue newerBigDialogue = Instantiate(dialogue2);
                    break;
            }
        }
    }
}
