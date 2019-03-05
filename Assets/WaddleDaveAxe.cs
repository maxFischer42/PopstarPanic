using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaddleDaveAxe : MonoBehaviour {

    public Rigidbody2D rigid;
    public Vector2 forceUp;
    public bool rotate = true;
    int z = 0;
    public bool movable = true;
    public GameObject effect;



	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
        if (!movable)
            return;
        rigid.AddForce(forceUp);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!rotate)
            return;
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 0f, z));
        z+= 5;
	}



    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && rigid.velocity.y != 0  && movable|| collision.gameObject.tag == "Player" && !movable)
        {
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(1, Vector2.up * 5f);
        }
        else if(collision.gameObject.tag == "Ground")
        {
            rigid.bodyType = RigidbodyType2D.Static;
            GameObject eff = (GameObject)Instantiate(effect,transform);
            Destroy(eff, 3.5f);
            rotate = false;
            Destroy(gameObject, 3.5f);
        }
    }
}
