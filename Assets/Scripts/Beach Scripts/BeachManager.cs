using UnityEngine;

public class BeachManager : MonoBehaviour
{
    public int progress;
    public ChangingRoom room;
    public BroTalking bro;
    public AudioSource source;
    public CameraController mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (mainCamera.roomNumber == 2)
        {
            source.Stop();
        }
    }

    public void UpdateProgress()
    {
        progress++;
        room.checker = false;
        bro.ChangeChecker();
    }
}
