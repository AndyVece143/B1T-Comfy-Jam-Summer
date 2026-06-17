using UnityEngine;

public class NPC : MonoBehaviour
{
    public BigDialogue dialogue1;
    public BigDialogue dialogue2;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (boxCollider.IsTouching(player.boxCollider))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (player.state != Player.State.NoMove)
                {
                    player.StopMoving(1);
                    player.talkIcon.enabled = false;
                    mainCamera.state = CameraController.State.StayStill;

                    switch (dialogueState)
                    {
                        case 0:
                            BigDialogue newBigDialogue = Instantiate(dialogue1);

                            if (dialogue2 == null)
                            {
                                checker = true;
                            }
                            else
                            {
                                dialogueState++;
                            }
                            break;
                        case 1:
                            if (dialogue2 != null)
                            {
                                BigDialogue newerBigDialogue = Instantiate(dialogue2);
                                checker = true;
                            }
                            break;
                    }
                }
            }
        }
    }
}
