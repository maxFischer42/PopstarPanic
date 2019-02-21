using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetColor : MonoBehaviour {

    public Material source;
	
	// Update is called once per frame
	void Update () {
        float R = source.color.r;
        float G = source.color.g;
        float B = source.color.b;
        Color color = new Color(R, G, B);
        PlayerPrefs.SetFloat("R", R);
        PlayerPrefs.SetFloat("G", G);
        PlayerPrefs.SetFloat("B", B);
        GetComponent<SpriteRenderer>().color = color;
    }
}
