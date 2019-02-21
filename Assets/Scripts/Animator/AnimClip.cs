using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AnimClip : ScriptableObject {

    public float frameRate = 0.6f;
    public Sprite[] cells;
}
