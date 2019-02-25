using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallJumpDetection : MonoBehaviour {

    public bool hitWall;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            hitWall = true;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            hitWall = true;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            hitWall = false;
        }
    }
}
