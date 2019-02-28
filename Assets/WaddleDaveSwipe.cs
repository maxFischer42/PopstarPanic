using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaddleDaveSwipe : MonoBehaviour {

    public WaddleDave dave;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player" && !dave.isAttacking)
        {
            dave.isAttacking = true;
        }
    }
}
