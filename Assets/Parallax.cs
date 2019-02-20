using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    public Vector2 offset;
    public float ratio = 1f;
	// Update is called once per frame
	void Update ()
    {
        transform.SetPositionAndRotation(offset * ratio, Quaternion.identity);
	}
}
