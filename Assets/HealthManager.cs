using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerController;

public class HealthManager : MonoBehaviour {

    public Image[] HPBars;
    public Sprite HPSprite;
    public int maxHP = 8;
    public int currentHP;
    private Controller controller;
    public bool takeDamage = false;
//    public Color myColor;
    public GameObject deathPrefab;
    public AudioClip kirbyHit;
    public bool start;
    public Text lives;
    public bool hit;
    public float hitstun = 1f;
    private float hitTimer;

    // Use this for initialization
    void Start()
    {
 //       GetComponent<SpriteRenderer>().color = myColor;
        if (!start)
            currentHP = PlayerPrefs.GetInt("HP");
        else
            currentHP = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("HP", currentHP);
        lives.text = PlayerPrefs.GetInt("1UP").ToString();
        UpdateUI();
        if(hit)
        {
            hitTimer += Time.deltaTime;
            if(hitTimer >= hitstun)
            {
                hit = false;
            }
        }
        if (takeDamage)
            TakeDamage(1, Vector2.zero);
    }

    public void gainHP(int hp)
    {
        if(currentHP + hp <= maxHP)
        {
            currentHP += hp;
        }
    }

    public void TakeDamage(int damage,Vector2 knockback)
    {
        if (damage == 1 && !hit)
        {
            GetComponent<Rigidbody2D>().velocity = knockback;
            currentHP -= damage;
            if (currentHP <= 0)
            {
                UpdateUI();
                Death();
            }
            else
                Camera.main.GetComponent<AudioSource>().PlayOneShot(kirbyHit);
            takeDamage = false;
        }
        else if((damage > 1 && hit) || (damage > 1 && !hit))
        {
            GetComponent<Rigidbody2D>().velocity = knockback;
            currentHP -= damage;
            if (currentHP <= 0)
            {
                UpdateUI();
                Death();
            }
            else
                Camera.main.GetComponent<AudioSource>().PlayOneShot(kirbyHit);
            takeDamage = false;
        }
        
    }

    public void Death()
    {
        PlayerPrefs.SetInt("1UP", PlayerPrefs.GetInt("1UP") - 1);
        PlayerPrefs.SetInt("HP", maxHP);
        GameObject prefab = (GameObject)Instantiate(deathPrefab, transform);
        prefab.transform.parent = null;
        prefab.GetComponent<DeathObject>().myColor = GetComponent<SpriteRenderer>().color;
        GameObject.FindObjectOfType<Camera>().GetComponent<AudioSource>().clip = null;
        GameObject.FindObjectOfType<Camera>().GetComponent<AudioSource>().Stop();
        gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        for(int i = 0; i < HPBars.Length;i++)
        {
            if(currentHP >= i + 1)
            {
                HPBars[i].enabled = true;
            }
            else
            {
                HPBars[i].enabled = false;
            }
        }
    }

}
