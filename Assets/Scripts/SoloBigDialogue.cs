using TMPro;
using UnityEngine;
using System.Collections;

public class SoloBigDialogue : MonoBehaviour
{
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI nameText;
    //public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    private int index;
    public int[] emotionChanges;
    public Canvas canvas;

    public Portrait character1;

    public float duration;
    public float moveDuration;

    private Vector3 character1Position;
    public GameObject textBox;
    private Vector3 textBoxPosition;
    private Vector3 character1EndPosition;
    private Vector3 textBoxEndPosition;
    public CameraController mainCamera;

    private const string HTML_ALPHA = "<color=#00000000>";
    public bool ready = false;
    public AudioClip audioClip;
    private bool ending = false;
    public Player player;
    public bool sceneTransition;
    public string sceneName;
    public LevelLoader loader;
    public bool canMove;
    public bool isChanging;
    public ChangingRoom room;
    public float dampSpeed = 7.0f;

    public bool isIntermission;
    private Intermission intermission;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
        mainText.text = string.Empty;
        mainCamera = CameraController.FindAnyObjectByType<CameraController>();
        mainCamera.state = CameraController.State.StayStill;

        nameText.text = "Lucy";
        player = Player.FindAnyObjectByType<Player>();
        if (isChanging)
        {
            room = ChangingRoom.FindAnyObjectByType<ChangingRoom>();
        }
        if (sceneTransition)
        {
            loader = LevelLoader.FindAnyObjectByType<LevelLoader>();
        }

        if (isIntermission)
        {
            intermission = Intermission.FindAnyObjectByType<Intermission>();
        }

        BeginningSprite();
        SetPositions();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (ready == true)
            {
                NextLine();
            }
        }

        if (Input.GetKeyDown(KeyCode.L) && ending == false)
        {
            StopAllCoroutines();
            StartCoroutine(MoveSpritesEnd());
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        ready = false;
        if (index < lines.Length - 1)
        {
            index++;
            ChangeEmotion();
            mainText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            StartCoroutine(MoveSpritesEnd());
        }
    }

    void BeginningSprite()
    {
        ChangeEmotion();
    }

    void ChangeEmotion()
    {
        character1.ChangeEmotion(emotionChanges[index]);
    }

    void SetPositions()
    {
        character1Position = character1.transform.position;
        textBoxPosition = textBox.transform.position;

        character1.transform.position = new Vector3(character1Position.x - 14f, character1Position.y, character1Position.z);
        textBox.transform.position = new Vector3(textBoxPosition.x, textBoxPosition.y - 6, textBoxPosition.z);

        character1EndPosition = character1.transform.position;
        textBoxEndPosition = textBox.transform.position;

        StartCoroutine(MoveSpritesBeginning());
    }

    IEnumerator TypeLine()
    {
        int i = 4;
        string originalText = lines[index];
        string displayedText = "";
        int alphaIndex = 0;

        foreach (char c in lines[index].ToCharArray())
        {
            alphaIndex++;
            mainText.text = originalText;
            displayedText = mainText.text.Insert(alphaIndex, HTML_ALPHA);
            mainText.text = displayedText;

            i++;
            if (i == 5)
            {
                SoundManager.instance.PlaySound(audioClip);
                i = 0;
            }

            yield return new WaitForSeconds(textSpeed);
        }
        ready = true;
    }

    IEnumerator MoveSpritesBeginning()
    {
        float time = 0;
        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            //character1.gameObject.transform.position = Vector3.Lerp(character1.gameObject.transform.position, character1Position, time / moveDuration);
            //textBox.transform.position = Vector3.Lerp(textBox.transform.position, textBoxPosition, time / moveDuration);
            character1.gameObject.transform.position = Vector3.Lerp(character1.gameObject.transform.position, character1Position, t);
            textBox.transform.position = Vector3.Lerp(textBox.transform.position, textBoxPosition, t);
            yield return null;
        }
        StartDialogue();
    }

    IEnumerator MoveSpritesEnd()
    {
        ending = true;
        float time = 0;
        while (time < moveDuration)
        {
            time += Time.deltaTime;
            float t = 1.0f - Mathf.Exp(-dampSpeed * Time.deltaTime);
            character1.gameObject.transform.position = Vector3.Lerp(character1.gameObject.transform.position, character1EndPosition, t);
            textBox.transform.position = Vector3.Lerp(textBox.transform.position, textBoxEndPosition, t);
            yield return null;
        }
        if (sceneTransition)
        {
            loader.LoadNextLevel(sceneName);
        }


        //gameUI.SetActive(true);
        if (canMove)
        {
            player.StartMoving();
            mainCamera.state = mainCamera.initialState;
        }

        if (isChanging)
        {
            room.TriggerChangeClothes();
        }

        if (isIntermission)
        {
            intermission.GoingToTurnOnLights();
        }

        Destroy(gameObject);
        //player.state = Player.State.Standard;
        //mainCamera.state = CameraController.State.FollowPlayer;
        //mainCamera.anim.enabled = false;
    }
}
