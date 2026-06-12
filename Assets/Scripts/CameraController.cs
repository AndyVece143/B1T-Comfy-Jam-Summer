using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.15f;
    private Vector3 velocity = Vector3.zero;

    public enum State
    {
        FollowPlayer,
        FollowPlayerSwimming,
        StayStill,
    }
    public State state;
    public State initialState;
    public int roomNumber;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        initialState = state;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.FollowPlayer:
                FollowPlayer();
                break;
            case State.StayStill:
                break;
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPosition = player.position + offset;
        targetPosition.y = 0;

        switch (roomNumber)
        {
            //Boardwalk
            case 0:
                if (targetPosition.x < 0)
                {
                    targetPosition.x = 0;
                }

                if (targetPosition.x > 22.2f)
                {
                    targetPosition.x = 22.2f;
                }
                break;

            //Beach
            case 1:
                if (targetPosition.x < 47.22f)
                {
                    targetPosition.x = 47.22f;
                }

                if (targetPosition.x > 72.78f)
                {
                    targetPosition.x = 72.78f;
                }
                break;
        }


        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
