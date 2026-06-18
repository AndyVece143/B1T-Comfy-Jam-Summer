using UnityEngine;

public class BroTalking : MonoBehaviour
{
    public BeachManager manager;
    public BigDialogue GoChangeClothes;
    public BigDialogue NiceClothes;
    public Player player;
    public BoxCollider2D boxCollider;
    public bool checker = false;
    public bool interactable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.IsTouching(player.boxCollider))
        {
            if (Input.GetKeyDown(KeyCode.Space) && interactable == true && player.state != Player.State.NoMove)
            {
                switch (manager.progress)
                {
                    case 1:
                        interactable = false;
                        player.talkIcon.enabled = false;
                        player.StopMoving(1);
                        BigDialogue newBigDialogue =Instantiate(GoChangeClothes);
                        newBigDialogue.bro = this;
                        checker = true;
                        break;
                    case 2:
                        interactable = false;
                        player.talkIcon.enabled = false;
                        player.StopMoving(1);
                        BigDialogue newerBigDialogue = Instantiate(NiceClothes);
                        newerBigDialogue.bro = this;
                        checker = true;
                        break;
                }
            }
        }
    }

    public void ChangeChecker()
    {
        checker = false;
        interactable = true;
    }
}
