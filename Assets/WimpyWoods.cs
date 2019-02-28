using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WimpyWoods : MonoBehaviour {

    public Sprite shoot;
    public Sprite Idle;
    public Sprite cut1;
    public Sprite cut2;
    public Sprite cut3;
    public Sprite destroyed;
    public GameObject waddleDave;
    private int current;

    public float timer;
    private SpriteRenderer s;

	// Use this for initialization
	void Start () {
        s = GetComponent<SpriteRenderer>();	
	}

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2 && current == 0)
        {
            s.sprite = Idle;
            current = 1;
            timer = 0;
        }
        else if (timer > 1 && current == 1)
        {
            s.sprite = cut1;
            current = 2;
            timer = 0;
        }
        else if (timer > 0.5f && current == 2)
        {
            s.sprite = cut2;
            current = 3;
            timer = 0;
        }
        else if (timer > 0.5f && current == 3)
        {
            s.sprite = cut3;
            current = 4;
            timer = 0;
        }
        else if (timer > 1f && current == 4)
        {
            s.sprite = destroyed;
            waddleDave.SetActive(true);
        }
    }
}
