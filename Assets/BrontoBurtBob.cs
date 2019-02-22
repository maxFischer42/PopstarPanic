using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrontoBurtBob : MonoBehaviour {

    private int direction = 0;  //0 down    1 idle - up     2 Up       3 Idle - down//
    public Transform upPos;
    public Transform midPos;
    public Transform downPos;
    private Vector3 _target;
    public float maxDistanceDelta = 1f;
    public float bobTimer;
    private float timer;

    private void Start()
    {
        _target = downPos.position;
    }

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        if(timer > bobTimer)
        {
            timer = 0f;
        }
        if (timer < bobTimer / 2)
        {
            _target = downPos.position;
        }
        else if(timer == bobTimer / 2)
        {
            _target = midPos.position;
        }
        else if(timer > bobTimer / 2)
        {
            _target = upPos.position;
        }
        Bob();
	}

    void Bob()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, maxDistanceDelta);
    }
}
