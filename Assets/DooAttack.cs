using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DooAttack : MonoBehaviour {

    public GameObject[] hurts;
    public float interval = 0.2f;
    public AudioClip dooAttack;
    public int current = 0;
    public float timer;
	
	// Update is called once per frame
	void Update () {
        if (current > hurts.Length)
            Destroy(gameObject);
        timer += Time.deltaTime;
        if(timer > interval)
        {
                hurts[current].SetActive(false);
                current++;
                hurts[current].SetActive(true);
            Camera.main.GetComponent<AudioSource>().PlayOneShot(dooAttack);
            timer = 0f;
        }
	}
}
