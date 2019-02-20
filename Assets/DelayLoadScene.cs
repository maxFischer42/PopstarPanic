using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayLoadScene : MonoBehaviour {
    public float delay;
    public string SceneToLoad;
    public bool skipDelay;
	// Use this for initialization
	void Start ()
    {
        StartCoroutine(Delay());
	}

    private void Update()
    {
        if(Input.GetButtonDown("Jump") && skipDelay)
        {
            SceneManager.LoadScene(SceneToLoad);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneToLoad);
    }
}
