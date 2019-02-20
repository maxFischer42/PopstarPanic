using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeSlider : MonoBehaviour {

    public bool opening;

    public void Start()
    {
        PlayerPrefs.SetFloat("Vol", 0.1f);
        if (opening)
           GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("Vol");
    }

    public void ChangeVolume(float vol)
    {
        PlayerPrefs.SetFloat("Vol", vol);
    }
}
