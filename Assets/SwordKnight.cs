using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordKnight : MonoBehaviour {

    public BoxCollider2D[] waypoints = new BoxCollider2D[2];
    public float speed;
    public enum State {Walk, Chase, Cut };
    public State currentState = State.Walk;
    private BoxCollider2D currentWaypoint;

    public 
        void Start()
    {
        currentWaypoint = waypoints[0];
    }
}
