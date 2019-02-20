using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathObject : MonoBehaviour {

    public float timer;
    public enum State { Freeze, Flying, Load };
    public State currentState = State.Freeze;
    private Rigidbody2D rigid;
    public float forceUp = 1f;
    public Color myColor;
    public AudioClip miss;
    public GameObject initialStars;


    // Use this for initialization
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = myColor;
        initialStars.transform.parent = null;
    }

    // Update is called once per frame
    void Update() {
        timer += Time.deltaTime;
        if (currentState == State.Freeze)
        {
            if (timer > 0.6f)
            {
                currentState = State.Flying;
                rigid.AddForce(new Vector2(0f, forceUp));
                GameObject.FindObjectOfType<Camera>().GetComponent<AudioSource>().PlayOneShot(miss);
            }
        }
        if (currentState == State.Flying)
        {
            transform.Rotate(0f, 0f, 7f);
            if (timer > 0.8f)
               rigid.gravityScale = 3f;
            if (timer > 4f)
            {
                timer = 0f;
                DontDestroyOnLoad(gameObject);
                Vector3 cameraPos = Camera.main.transform.parent.transform.position;
                if (PlayerPrefs.GetInt("1UP") >= 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    Camera.main.transform.parent.SetPositionAndRotation(cameraPos, Quaternion.identity);
                    Destroy(gameObject);
                }
                else
                {
                    SceneManager.LoadScene("Title");
                }
            }
        }
	}
}
