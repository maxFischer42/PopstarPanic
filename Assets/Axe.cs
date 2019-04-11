using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class Axe : MonoBehaviour {

    public Controller playerController;
    public GameObject axe;
    public AudioClip sound;
    public bool cooldown;


	
	// Update is called once per frame
	void Update ()
    {
		if(playerController.currentState == Controller.State.Falling && Input.GetButtonDown("Attack") && cooldown)
        {
            GameObject newAxe = (GameObject)Instantiate(axe, transform);
            newAxe.transform.parent = null;
            newAxe.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * -2;
            Destroy(newAxe, 7f);
            StartCoroutine(Cool());
        }
	}
    public IEnumerator Cool()
    {
        cooldown = false;
        yield return new WaitForSeconds(5f);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
        cooldown = true;
    }


}
