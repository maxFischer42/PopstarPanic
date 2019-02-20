using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueSong : MonoBehaviour {

    public AudioSource aud;
    public bool m_continue = true;

	// Use this for initialization
	void Start () {
        if (m_continue)
        {
            Debug.Log("Resume Song");
            aud.time = PlayerPrefs.GetFloat("song");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        PlayerPrefs.SetFloat("song", aud.time);
	}
}
