using UnityEngine;

public class GoldTrigger : MonoBehaviour
{
    public Player player;
    public BoxCollider2D boxCollider;
    public SoloBigDialogue bigGold;
    public SoloBigDialogue smallGold;
    public GameObject potOfGold;
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

            switch (StaticData.beganAtBeginning)
            {
                case true:
                    SoloBigDialogue newDialogue = Instantiate(bigGold);
                    break;
                case false:
                    SoloBigDialogue newerDialogue = Instantiate(smallGold);
                    break;
            }
        }
    }

    public void HideGold()
    {
        potOfGold.SetActive(false);
    }
}
