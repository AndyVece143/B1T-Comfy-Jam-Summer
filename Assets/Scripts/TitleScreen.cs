using System.Collections;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject mainTitle;
    public GameObject credits;
    public GameObject controls;
    public GameObject chapters;
    public float duration;

    private float buttonTimer;
    public LevelLoader loader;
    public float moving;
    public float dampSpeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log(mainTitle.transform.position);
        Debug.Log(credits.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        buttonTimer -= Time.deltaTime;
    }

    public void SlideLeft()
    {
        if (buttonTimer <= 0)
        {
            StartCoroutine(MoveElements(false));
            buttonTimer = duration;
        }
    }

    public void SlideRight()
    {
        if (buttonTimer <= 0)
        {
            StartCoroutine(MoveElements(true));
            buttonTimer = duration;
        }
    }

    public void StartGame(int i)
    {
        switch (i)
        {
            case 0:
                loader.LoadNextLevel("PrePrologue");
                break;
            case 1:
                loader.LoadNextLevel("Level1");
                break;
            case 2:
                loader.LoadNextLevel("Intermission1");
                break;
            case 3:
                loader.LoadNextLevel("Level2");
                break;
            case 4:
                loader.LoadNextLevel("Intermission2");
                break;
            case 5:
                loader.LoadNextLevel("Level3");
                break;
            case 6:
                loader.LoadNextLevel("Epilogue");
                break;
        }
    }

    IEnumerator MoveElements(bool right)
    {
        float time = 0;
        float moveAmount = moving;

        if (!right)
        {
            moveAmount = -moving;
        }

        Vector2 titleVector = new Vector2(mainTitle.transform.position.x + moveAmount, 0);
        Vector2 creditsVector = new Vector2(credits.transform.position.x + moveAmount, 0);
        Vector2 controlsVector = new Vector2(controls.transform.position.x + moveAmount, 0);
        Vector2 chaptersVector = new Vector2(chapters.transform.position.x + moveAmount, 0);

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);

            mainTitle.gameObject.transform.position = Vector2.Lerp(mainTitle.transform.position, titleVector, t);
            credits.gameObject.transform.position = Vector2.Lerp(credits.transform.position, creditsVector, t);
            controls.gameObject.transform.position = Vector2.Lerp(controls.transform.position, controlsVector, t);
            chapters.gameObject.transform.position = Vector2.Lerp(chapters.transform.position, chaptersVector, t);

            yield return null;
        }
    }
}
