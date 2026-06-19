using UnityEngine;

public class SuperBubbleSpawner : MonoBehaviour
{
    public Bubble bubble;
    public Transform spawnPosition;
    private float timer;
    public float timerMax;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            SpawnBubble();
            timer = timerMax;
        }
    }

    void SpawnBubble()
    {
        Bubble newBubble = Instantiate(bubble);
        newBubble.transform.position = spawnPosition.position;
    }
}
