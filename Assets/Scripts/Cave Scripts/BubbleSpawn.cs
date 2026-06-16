using UnityEngine;

public class BubbleSpawn : MonoBehaviour
{
    public Bubble bubble;
    public Bubble childBubble;

    public float timer;
    public float timerMax;
    public Transform bubbleSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (childBubble == null)
        {
            timer -= Time.deltaTime;
        }

        if (childBubble == null && timer < 0)
        {
            Bubble newBubble = Instantiate(bubble);
            childBubble = newBubble;
            newBubble.transform.position = bubbleSpawn.position;
            timer = timerMax;
        }
    }
}
