using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbilityGet : MonoBehaviour {

    public Image fade;
    public bool fading;
    public string SceneToLoad;
    public GameObject particles;
    public SpriteRenderer spriteRender;
	
	// Update is called once per frame
	void Update ()
    {
        if (!fading)
            return;
            Color newColor = fade.color;
            newColor.a += 0.01f;
            fade.color = newColor;
        if(newColor.a >= 1)
        {
            SceneManager.LoadScene(SceneToLoad);
        }
        
	}

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            spriteRender.enabled = false;
            particles.SetActive(false);
            fading = true;
        }
    }
}
