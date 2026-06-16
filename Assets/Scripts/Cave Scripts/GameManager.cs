using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SwimmingPlayer player;
    public Vector2 respawnPosition;
    public Checkpoint activeCheckpoint;
    public RoomTransition transition;
    public CameraController mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        respawnPosition = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator RespawnPlayer()
    {
        transition.FadeToBlack();
        yield return new WaitForSeconds(1.1f);

        if (activeCheckpoint)
        {
            player.transform.position = new Vector2(activeCheckpoint.transform.position.x, activeCheckpoint.transform.position.y + 2);
        }
        else
        {
            player.transform.position = respawnPosition;
        }
        player.state = SwimmingPlayer.State.Standard;
        player.air = 45;
        mainCamera.state = CameraController.State.FollowPlayerSwimming;
    }
}
