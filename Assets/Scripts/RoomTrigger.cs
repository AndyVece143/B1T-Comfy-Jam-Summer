using System.Collections;
using UnityEngine;

public class RoomTrigger : MonoBehaviour
{
    public Player player;
    public BoxCollider2D boxCollider;
    public CameraController mainCamera;
    public RoomTransition roomTransition;
    public int roomNumber;
    public Transform teleportPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = Player.FindAnyObjectByType<Player>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public IEnumerator BeginRoomTransition()
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
