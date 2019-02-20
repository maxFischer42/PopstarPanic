using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotzo : MonoBehaviour {

    public GameObject cannonBall;
    public GameObject particle;

    public float fireRate;
    private float timer;

    public Sprite left;
    public Sprite leftUp;
    public Sprite up;

    public Transform[] spawns;

    private SpriteRenderer sprt;
    public float speed;
    private Vector2 currentDir;

    private void Start()
    {
        sprt = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update ()
    {
        int i = 1;
        Vector3 playerLoc = GameObject.Find("Player").transform.position;
        Vector3 distance = playerLoc - transform.position;
        //distance.Normalize();
        if (distance.x > 0 && distance.x < 1)
        {
            currentDir = new Vector2(1, 1);
            i = 2;
            sprt.sprite = leftUp;
            sprt.flipX = true;

        }
        else if (distance.x > 1)
        {
            currentDir = new Vector2(1, 0);
            sprt.sprite = left;
            sprt.flipX = true;
            i = 1;
        }
        else if (distance.x < 1 && distance.x > -1)
        {
            currentDir = new Vector2(0, 1);
            sprt.sprite = up;
            sprt.flipX = false;
            i = 0;
        }
        else if (distance.x < 0 && distance.x > -1)
        {
            currentDir = new Vector2(-1, 1);
            sprt.sprite = leftUp;
            sprt.flipX = false;
            i = 3;
        }
        else if (distance.x < -1)
        {
            currentDir = new Vector2(-1, 0);
            sprt.sprite = left;
            sprt.flipX = false;
            i = 4;
        }

        timer += Time.deltaTime;
        if(timer > fireRate)
        {
            GameObject smoke = (GameObject)Instantiate(particle, spawns[i]);
            Destroy(smoke, 0.2f);
            GameObject ball = (GameObject)Instantiate(cannonBall, spawns[i]);
            ball.GetComponent<Rigidbody2D>().velocity = currentDir * speed ;
            Destroy(ball, fireRate);
            timer = 0f;
        }
	}
}
