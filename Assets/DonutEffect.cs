using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class DonutEffect : MonoBehaviour
{
    public Curve shakeCurve;
    public float timeToFall;
    private bool active = false;
    private float timer;
    private Rigidbody2D rigidBody;


    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void StartShake()
    {
        if (active)
            return;
        active = true;
    }

    public void Update()
    {
        if (!active)
            return;
        transform.localPosition = new Vector2(transform.localPosition.x + (shakeCurve.x.Evaluate(timer)/25), transform.localPosition.y + (shakeCurve.y.Evaluate(timer)/25));
        timer += Time.deltaTime;
        if (timer >= timeToFall)
        {
            Fall();
            enabled = false;
        }
    }

    public void Fall()
    {
        rigidBody.gravityScale = 3f;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Controller>())
        {
            StartShake();
        }
    }
}

