using System.Collections;
using UnityEngine;

public class Intermission2 : MonoBehaviour
{
    public Player player;
    public SoloBigDialogue dialogue;
    private bool isMusicPlaying = false;
    public AudioSource source;
    public CameraController mainCamera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.StopMoving(1);
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.state != Player.State.NoMove && isMusicPlaying == false)
        {
            Debug.Log("BeginMusic!");
            isMusicPlaying = true;
            source.Play();
            mainCamera.state = CameraController.State.FollowPlayerIntermission;
        }
    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1.5f);
        SoloBigDialogue newDialogue = Instantiate(dialogue);
    }
}
