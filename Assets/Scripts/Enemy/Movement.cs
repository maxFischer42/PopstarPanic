using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    public bool direction;
    public float destroy = 1f;
    public float speed = 1f;
    public float maxDistanceDelta = 2f;
    Vector3 dir;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, destroy);
        if (direction)
            dir = new Vector2(-2, 0);
        else
            dir = new Vector2(2, 0);
    }

	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + dir, maxDistanceDelta);
    }
}
