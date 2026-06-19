using System.Collections;
using UnityEngine;

public class PrePrologueManager : MonoBehaviour
{
    public SoloBigDialogue dialogue;
    public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StaticData.beganAtBeginning = true;
        StartCoroutine(StartCutscene());
    }

    public void GoToNextScene()
    {
        loader.LoadNextLevel("Prologue");
    }

    private IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(1);
        SoloBigDialogue newDialogue = Instantiate(dialogue);
    }
}
