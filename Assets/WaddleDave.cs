using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaddleDave : MonoBehaviour {

    public RuntimeAnimatorController walking;
    public Sprite throwing;
    public Sprite swipeBefore;
    public Sprite swipeAfter;
    private Animator anim;
    private SpriteRenderer sprt;
    private float timer;
    public bool flip;
    private Vector2 direction;
    public float moveSpeed;
    public enum State {Moving, throwing, swipe };
    public State currentState = State.Moving;
    public float maxDistanceDelta = 0.1f;
    public bool isAttacking;
    private Rigidbody2D rig;
    float throwtimer;
    public GameObject axe;
    public Transform axeSpawn;
    float swipeTimer;
    public GameObject hitbox;
    public Transform hitSpawn;
    public int lowball = 990;

    public int HP = 15;
    // Use this for initialization
    void Start () {
        flip = false;
        anim = GetComponent<Animator>();
        sprt = GetComponent<SpriteRenderer>();
        direction = new Vector2(1, 0f);
        rig = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if(isAttacking)
        {
            currentState = State.swipe;
            swipeTimer += Time.deltaTime;
        }
        if(timer > 8f)
        {            
            flip = !flip;
            timer = 0f;
        }
        sprt.flipX = flip;
        int r = Random.Range(0, 1000);
        if (r > lowball && !isAttacking)
        {

            currentState = State.throwing;

            GameObject ax = (GameObject)Instantiate(axe, axeSpawn);
            ax.transform.parent = null;
            ax.transform.localScale = axe.transform.localScale;
        }
        GetState();
    }

    void GetState()
    {
        switch(currentState)
        {
            case State.Moving:
                anim.enabled = true;
                anim.runtimeAnimatorController = walking;
                Move(direction, flip);
                break;
            case State.throwing:
                throwtimer += Time.deltaTime;
                anim.enabled = false;
                sprt.sprite = throwing;
                if (throwtimer > 1f)
                {
                    throwtimer = 0f;
                    currentState = State.Moving;
                }
                break;
            case State.swipe:
                anim.enabled = false;
                if (swipeTimer < 0.5f)
                {
                    sprt.sprite = swipeBefore;
                }
                else if(swipeTimer > 0.5f)
                {
                    sprt.sprite = swipeAfter;
                    if(isAttacking)
                    {
                        GameObject hb = (GameObject)Instantiate(hitbox, hitSpawn);
                        Destroy(hb, 0.1f);
                        isAttacking = false;
                        swipeTimer = 0f;
                        currentState = State.Moving;
                    }
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cutter")
        {
            HP--;
            Destroy(collision.gameObject);
            if (HP < 7)
            {
                maxDistanceDelta *= 1.2f;
                lowball -= 5;
            }
            if (HP <= 0)
                Destroy(gameObject);
        }
    }

    private void Move(Vector3 direction, bool flip)
    {
        Vector3 dir = direction;
        dir *= moveSpeed;
        if(!flip)
        {
            dir.x *= -1;
        }
        transform.position = Vector2.MoveTowards(transform.position, transform.position + dir, maxDistanceDelta);
    }
}
