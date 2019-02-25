using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class DetectGround : MonoBehaviour
{
    public Controller controller;
    public bool hitGround;
    public float countDown;
    private float timer;

    private void Start()
    {
        GetComponent<Collider2D>().offset = new Vector2(-0.01f, -0.3f);
        GetComponent<BoxCollider2D>().size = new Vector2(1f, 1f);

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" && controller.finishJump || collision.gameObject.tag == "Ground" && controller.currentState == Controller.State.Falling)
        {
            controller.isGrounded = true;
            controller.finishJump = false;
            controller.GetComponent<Rigidbody2D>().velocity = new Vector2(controller.GetComponent<Rigidbody2D>().velocity.x, 0f);
            hitGround = true;
        }
        if (collision.gameObject.tag == "Ground")
        {
            timer = 0;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        { 
            controller.transform.Translate(0, 1f, 0);
            controller.currentState = Controller.State.Jumping;
            
        }
        if(collision.gameObject.tag == "Ground")
        {
            timer += Time.deltaTime;
            if (timer >= countDown)
            {
                controller.currentJumps = 0;
                timer = 0;
            }
        }
        if (collision.gameObject.tag == "Ground" && controller.finishJump || collision.gameObject.tag == "Ground" && controller.currentState == Controller.State.Falling)
        {
            
            hitGround = true;
            controller.isGrounded = true;
            controller.finishJump = false;
            controller.GetComponent<Rigidbody2D>().velocity = new Vector2(controller.GetComponent<Rigidbody2D>().velocity.x, 0f);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //controller.isGrounded = false;
            //controller.currentState = Controller.State.
            //controller.finishJump = false;
            hitGround = false;
        }
    }
}
