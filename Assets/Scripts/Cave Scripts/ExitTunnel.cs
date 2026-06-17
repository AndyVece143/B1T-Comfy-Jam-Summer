using UnityEngine;

public class ExitTunnel : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public LevelLoader loader;
    public string sceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            loader.LoadNextLevel(sceneName);
        }
    }
}
