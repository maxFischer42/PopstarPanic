using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LIVES : MonoBehaviour {

    public int _ID;
    public LifeList lifeObject;
    public AudioClip sound;

    private void Start()
    {
        if (lifeObject.lives[_ID] == true)
            gameObject.SetActive(false);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPrefs.SetInt("1UP", PlayerPrefs.GetInt("1UP") + 1);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
            lifeObject.lives[_ID] = true;
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 500);
            Destroy(gameObject);
        }
    }
}
