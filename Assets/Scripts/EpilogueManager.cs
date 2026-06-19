using System.Collections;
using UnityEngine;

public class EpilogueManager : MonoBehaviour
{
    public SoloBigDialogue dialogue1;
    public SoloBigDialogue dialogue2;
    public LevelLoader loader;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(StartCutscene());
    }

    public void BackToTitle()
    {
        loader.LoadNextLevel("Title");
    }

    private IEnumerator StartCutscene()
    {
        yield return new WaitForSeconds(1);

        switch (StaticData.beganAtBeginning)
        {
            case true:
                SoloBigDialogue newDialogue = Instantiate(dialogue1);
                break;
            case false:
                SoloBigDialogue newerDialogue = Instantiate(dialogue2);
                break;

        }
    }
}
