using System.Collections;
using UnityEngine;

public class EndingManager : MonoBehaviour
{
    public SoloBigDialogue dialogue;
    public Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player.StopMoving(1);
        StartCoroutine(Cutscene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Cutscene()
    {
        yield return new WaitForSeconds(1.5f);
        SoloBigDialogue newDialogue = Instantiate(dialogue);
    }
}
