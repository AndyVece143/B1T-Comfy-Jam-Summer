using System.Collections;
using UnityEngine;

public class SandwichShop : MonoBehaviour
{
    public BeachManager manager;
    public BigDialogue buySandwich;
    public BigDialogue buySandwichBikini;
    public BigDialogue afterSandwich;
    public BigDialogue afterSandwichBikini;
    public Player player;
    public BoxCollider2D boxCollider;
    public bool checker = false;
    public bool interactable;
    public bool boughtSandwich = false;
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
                if (boughtSandwich == true)
                {
                    switch (manager.progress)
                    {
                        case 0:
                        case 1:
                            interactable = false;
                            player.talkIcon.enabled = false;
                            player.StopMoving(1);

                            BigDialogue newBigDialogue = Instantiate(afterSandwich);
                            break;

                        case 2:
                            interactable = false;
                            player.talkIcon.enabled = false;
                            player.StopMoving(1);

                            BigDialogue newerBigDialogue = Instantiate(afterSandwichBikini);
                            break;
                    }
                    checker = true;
                }

                else
                {
                    switch (manager.progress)
                    {
                        case 0:
                        case 1:
                            interactable = false;
                            player.talkIcon.enabled = false;
                            player.StopMoving(1);

                            BigDialogue newBigDialogue = Instantiate(buySandwich);
                            break;

                        case 2:
                            interactable = false;
                            player.talkIcon.enabled = false;
                            player.StopMoving(1);

                            BigDialogue newerBigDialogue = Instantiate(buySandwichBikini);
                            break;
                    }
                }
            }
        }
    }

    public void UpdateSandwich()
    {
        boughtSandwich = true;
        interactable = true;
        //checker = true;
    }
}
