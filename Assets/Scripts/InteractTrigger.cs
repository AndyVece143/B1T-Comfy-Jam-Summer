using UnityEngine;

public class InteractTrigger : MonoBehaviour
{
    public Player player;
    public string[] dialogue;
    public InspectBox inspectBox;

    public bool triggered;
    public BoxCollider2D boxCollider;
    public int react;
    public CameraController mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
        triggered = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player.gameObject && triggered == false)
        {
            triggered = true;
            player.StopMoving(1);

            InspectBox newInspectBox = Instantiate(inspectBox);
            newInspectBox.lines = dialogue;
        }
    }
}
