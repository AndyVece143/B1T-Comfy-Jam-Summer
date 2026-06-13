using System.Collections;
using Unity.VectorGraphics;
using Unity.VisualScripting;
using UnityEngine;

public class ChangingRoom : MonoBehaviour
{
    public BeachManager manager;
    public BoxCollider2D boxCollider;
    public Player player;
    public CameraController mainCamera;
    public bool interactable;
    public bool checker = false;
    public string[] dialogue1;
    public string[] dialogue2;
    public InspectBox inspectBox;
    public SoloBigDialogue BeforeChanging;
    public SoloBigDialogue AfterChanging;
    public RoomTransition roomTransition;

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
            if (Input.GetKeyDown(KeyCode.Space) && interactable == true && player.state != Player.State.NoMove)
            {
                switch (manager.progress)
                {
                    case 0:
                        interactable = false;
                        player.inspectIcon.enabled = false;
                        mainCamera.state = CameraController.State.StayStill;
                        player.StopMoving(1);

                        InspectBox newInspectBox = Instantiate(inspectBox);
                        newInspectBox.lines = dialogue1;
                        newInspectBox.room = this;
                        checker = true;
                        break;
                    case 1:
                        interactable = false;
                        player.inspectIcon.enabled = false;
                        player.StopMoving(1);
                        SoloBigDialogue newBigDialogue = Instantiate(BeforeChanging);
                        break;
                    case 2:
                        interactable = false;
                        player.inspectIcon.enabled = false;
                        mainCamera.state = CameraController.State.StayStill;
                        player.StopMoving(1);

                        InspectBox newerInspectBox = Instantiate(inspectBox);
                        newerInspectBox.lines = dialogue2;
                        newerInspectBox.room = this;
                        checker = true;
                        break;
                }

                //interactable = false;
                //player.inspectIcon.enabled = false;
                //mainCamera.state = CameraController.State.StayStill;
                //player.StopMoving(react);

                //InspectBox newInspectBox = Instantiate(inspectBox);
                //newInspectBox.lines = dialogue;
                //newInspectBox.interactableObject = this;
                //checker = true;
            }
        }
    }

    public void TriggerChangeClothes()
    {
        StartCoroutine(ChangeClothes());
    }

    public IEnumerator ChangeClothes()
    {
        roomTransition.FadeToBlack();
        yield return new WaitForSeconds(1.1f);
        Debug.Log("Awooga");
        player.bikini = true;
        yield return new WaitForSeconds(1.4f);
        SoloBigDialogue newBigDialogue = Instantiate(AfterChanging);
        interactable = true;
        manager.UpdateProgress();
    }
}
