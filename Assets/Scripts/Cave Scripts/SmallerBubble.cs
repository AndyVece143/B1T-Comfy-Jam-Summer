using UnityEngine;

public class SmallerBubble : MonoBehaviour
{
    public Rigidbody2D body;
    public float speed;
    public float timer;

    public float amplitude;
    public float frequency;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xSpeed = amplitude * frequency * Mathf.Cos(Time.time * frequency);
        body.linearVelocity = new Vector2(xSpeed, speed);
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            Destroy(gameObject);
        }
    }
}
