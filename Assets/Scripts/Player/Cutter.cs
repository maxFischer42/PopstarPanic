using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class Cutter : MonoBehaviour {

    public float cooldown;
    public float timeToShoot = 0.1f;
    private float cooldownCurrent;
    public GameObject cutter;
    bool flip = false;
    public bool throwing;
    public AudioClip sound;


    // Update is called once per frame
    void Update ()
    {
        cooldownCurrent++;
        flip = GetComponent<SpriteRenderer>().flipX;
	}

    public void CutCommand()
    {
        if (cooldown > cooldownCurrent || GetComponent<Controller>().currentState == Controller.State.Jumping)
            return;
        throwing = true;
        StartCoroutine(Cut());
    }

    public IEnumerator Cut()
    {
        cooldownCurrent = 0f;
        yield return new WaitForSeconds(timeToShoot);
        GameObject cut = (GameObject)Instantiate(cutter,transform);
        Camera.main.GetComponent<AudioSource>().PlayOneShot(sound);
        cut.transform.parent = null;
        cut.GetComponent<Movement>().direction = flip;
        throwing = false;
        
    }




}
