using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour {

    public Transform target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.SetPositionAndRotation(target.transform.position, Quaternion.identity);
    }
}
