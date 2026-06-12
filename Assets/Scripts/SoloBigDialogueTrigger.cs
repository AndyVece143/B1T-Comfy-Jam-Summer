using UnityEngine;

public class SoloBigDialogueTrigger : MonoBehaviour
{
    public Player player;
    public BoxCollider2D boxCollider;
    public SoloBigDialogue bigDialogue;
    private bool triggered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered == false && collision.gameObject == player.gameObject)
        {
            triggered = true;
            player.StopMoving(1);
            SoloBigDialogue newBigDialogue = Instantiate(bigDialogue);
        }
    }
}
