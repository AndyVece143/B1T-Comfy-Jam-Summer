using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Intermission : MonoBehaviour
{
    public Player player;
    public Light2D playerLight;
    public Light2D[] lights;
    public Light2D globalLight;
    public SoloBigDialogue beginning;
    public SoloBigDialogue lightsAreOn;
    public AudioClip click;
    private bool isMusicPlaying = false;
    public AudioSource source;
    public CameraController mainCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TurnOffLights();
        player.StopMoving(1);
        StartCoroutine(ItsDark());
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

    IEnumerator ItsDark()
    {
        yield return new WaitForSeconds(1.5f);
        SoloBigDialogue newDialogue = Instantiate(beginning);
    }

    IEnumerator LetsGetBright()
    {
        yield return new WaitForSeconds(0.5f);
        playerLight.enabled = false;
        SoundManager.instance.PlaySound(click);
        player.StopMoving(2);
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(TurnOnLights());

        //yield return new WaitForSeconds(6);

    }

    void TurnOffLights()
    {
        for (int i = 0; i < lights.Length -1; i++)
        {
            lights[i].enabled = false;
        }
        globalLight.enabled = false;
    }

    IEnumerator TurnOnLights()
    {
        //for (int i = 0; i < lights.Length - 1; i++)
        //{
        //    lights[i].enabled = true;
        //}
        //playerLight.enabled = true;
        globalLight.enabled = true;
        foreach (var light in lights)
        {
            light.enabled = true;
            SoundManager.instance.PlaySound(click);

            yield return new WaitForSeconds(0.2f);
        }

        yield return new WaitForSeconds(1.5f);
        FinishCutscene();
    }

    void FinishCutscene()
    {
        SoloBigDialogue newDialogue = Instantiate(lightsAreOn);
    }

    public void GoingToTurnOnLights()
    {
        StartCoroutine(LetsGetBright());
    }
}
