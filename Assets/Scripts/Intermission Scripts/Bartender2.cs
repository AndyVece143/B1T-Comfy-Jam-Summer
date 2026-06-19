using UnityEngine;

public class Bartender2 : MonoBehaviour
{
    public BigDialogue drinkDialogue1;
    public BigDialogue drinkDialogue2;
    public BigDialogue noDrinkDialogue1;
    public BigDialogue noDrinkDialogue2;

    public int dialogueState = 0;
    public Player player;
    public BoxCollider2D boxCollider;
    public bool interactable;

    public bool checker = false;
    public CameraController mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
        interactable = true;
        //StaticData.drankWithBartender1 = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.IsTouching(player.boxCollider) && Input.GetKeyDown(KeyCode.Space) && player.state != Player.State.NoMove)
        {
            player.StopMoving(1);
            player.talkIcon.enabled = false;
            mainCamera.state = CameraController.State.StayStill;

            switch (StaticData.drankWithBartender1)
            {
                case true:
                    switch (dialogueState)
                    {
                        case 0:
                            BigDialogue newBigDialogue = Instantiate(drinkDialogue1);
                            dialogueState++;
                            break;
                        case 1:
                            BigDialogue newerBigDialogue = Instantiate(drinkDialogue2);
                            checker = true;
                            break;
                    }
                    break;
                case false:
                    switch (dialogueState)
                    {
                        case 0:
                            BigDialogue noDrink = Instantiate(noDrinkDialogue1);
                            dialogueState++;
                            break;
                        case 1:
                            BigDialogue stillNoDrink = Instantiate(noDrinkDialogue2);
                            checker = true;
                            break;
                    }
                    break;
            }
        }
    }
}
