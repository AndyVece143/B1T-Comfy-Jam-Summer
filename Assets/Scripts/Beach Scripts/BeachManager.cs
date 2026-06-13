using UnityEngine;

public class BeachManager : MonoBehaviour
{
    public int progress;
    public ChangingRoom room;
    public BroTalking bro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateProgress()
    {
        progress++;
        room.checker = false;
        bro.ChangeChecker();
    }
}
