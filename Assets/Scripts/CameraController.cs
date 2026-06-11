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

        if (targetPosition.x < 0)
        {
            targetPosition.x = 0;
        }

        if (targetPosition.x > 22.2f)
        {
            targetPosition.x = 22.2f;
        }

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
