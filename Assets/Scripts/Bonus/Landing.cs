using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Landing : MonoBehaviour {
    Vector3 Origin;
    public Text text;
    public AudioClip m_short;
    public AudioClip miss;
    public AudioClip m_long;
    public GameObject Kirbflag;
    public string sceneToLoad = "Map";
    public Vector3 offset;

    bool ended;
    float countdown;
	// Use this for initialization
	void Start () {
        Origin = transform.position;
	}
    private void Update()
    {
        if (ended)
            countdown += Time.deltaTime;
        if (countdown >= 6.5f)
            SceneManager.LoadScene(sceneToLoad);

    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            float dis = Mathf.Round(transform.position.x - Origin.x);
            string distance = dis.ToString() + " M";
            text.text = distance;
            AudioClip toPlay;
            if(dis >= 25)
            {
                toPlay = m_long;
                GameObject flagKirb = (GameObject)Instantiate(Kirbflag, transform);
                flagKirb.transform.position += offset;
                flagKirb.transform.parent = null;
            }
            else if(dis >= 10)
            {
                toPlay = m_short;
            }
            else
            {
                toPlay = miss;
            }
            GetComponent<Animator>().enabled = false;
            Camera.main.GetComponent<AudioSource>().Stop();
            Camera.main.GetComponent<AudioSource>().PlayOneShot(toPlay);
            ended = true;
        }
    }


}
