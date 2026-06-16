using System.Collections;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public BoxCollider2D boxCollider;
    public Animator anim;
    public Transform orbSpawn;
    public Transform orbSpawn2;
    private float timer;
    public float timerMax;
    public bool firing = false;
    public Orb orb;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        timer = timerMax;
    }

    // Update is called once per frame
    void Update()
    {
        if (firing == false)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                firing = true;
                StartCoroutine(SpawnOrbs());
            }
        }

        anim.SetBool("firing", firing);
    }

    IEnumerator SpawnOrbs()
    {
        yield return new WaitForSeconds(1);
        Orb orb1 = Instantiate(orb);
        orb1.transform.position = orbSpawn.position;
        orb1.goingRight = true;
        orb1.startingBoost = speed;

        Orb orb2 = Instantiate(orb);
        orb2.transform.position = orbSpawn2.position;
        orb2.goingRight = false;
        orb2.startingBoost = speed;

        yield return new WaitForSeconds(0.5f);
        firing = false;
        timer = timerMax;
    }
}
