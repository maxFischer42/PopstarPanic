using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurt : MonoBehaviour {

    public Vector2 kbi;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector2 kb = transform.position - collision.gameObject.GetComponent<Transform>().position;
            Vector2 knock;
            if (kb.x > 0)
            {
                knock = new Vector2(-1, 1);
            }
            else
            {
                knock = new Vector2(1, 1);
            }
            knock *= kbi;
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(1, knock);
        }
    }
}
