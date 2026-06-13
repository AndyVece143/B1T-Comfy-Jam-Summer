using System.Collections;
using UnityEngine;

public class RoomTriggerAgain : MonoBehaviour
{
    public Player player;
    public BoxCollider2D boxCollider;
    public CameraController mainCamera;
    public RoomTransition roomTransition;
    public int roomNumber;
    public Transform teleportPoint;
    public bool interactable;
    public BeachManager manager;
    public InspectBox inspectBox;
    public string[] noBikiniDialogue;

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
            if (Input.GetKeyDown(KeyCode.Space) && interactable == true)
            {
                switch (manager.progress)
                {
                    case 0:
                    case 1:
                        interactable = false;
                        player.goIcon.enabled = false;
                        player.StopMoving(1);
                        InspectBox newInspectBox = Instantiate(inspectBox);
                        newInspectBox.lines = noBikiniDialogue;
                        newInspectBox.caveEntrance = this;
                        break;
                    case 2:
                        interactable = false;
                        StartCoroutine(RoomTransition());
                        break;
                }

            }
        }
    }

    public IEnumerator RoomTransition()
    {
        player.StopMoving(1);
        roomTransition.BecomeTrans();
        yield return new WaitForSeconds(1.1f);
        player.transform.position = teleportPoint.position;
        mainCamera.roomNumber = roomNumber;
        yield return new WaitForSeconds(1.4f);
        player.StartMoving();
    }
}
