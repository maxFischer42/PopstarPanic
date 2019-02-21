using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mash : MonoBehaviour {

    public float duration = 5f;
    float timer;
    public int presses;
    public Sprite mthrow;
    public SpriteRenderer kirb;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if(Input.GetButtonDown("Attack"))
        {
            presses++;
        }
        if(duration < timer)
        {
            kirb.sprite = mthrow;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 3) * presses);
            GetComponent<SpriteRenderer>().enabled = true;
            enabled = false;
        }
	}
}
