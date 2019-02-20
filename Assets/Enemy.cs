using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Enemy : ScriptableObject {

    public Vector3 direction;
    public Vector2 knockback;
    public bool noPlayer;

}
