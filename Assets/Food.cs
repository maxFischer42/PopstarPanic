using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public int restoreAmount;
    public AudioClip sound;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 150);
            collision.GetComponent<HealthManager>().gainHP(restoreAmount);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
            Destroy(gameObject);
        }
    }


}
