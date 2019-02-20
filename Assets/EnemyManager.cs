using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public Enemy myEnemy;
    public GameObject DeathEffect;
    public float maxDistanceDelta = 1.0f;
    public Transform dir;
    public AudioClip hitSoundCutter;
    public bool block;
    public bool isDoo;
    public float dooTimer;
    private bool dooStop;
    private bool spawnedHurt;
    public RuntimeAnimatorController defaultDoo;
    public RuntimeAnimatorController attackDoo;
    public GameObject hurtObj;
    public Transform hurtSpawn;
    public bool invincible;

	
	// Update is called once per frame
	void Update () {
        if ((transform.position - GameObject.Find("Player").gameObject.GetComponent<Transform>().position).x < 15 && !myEnemy.noPlayer && !dooStop)
        transform.position = Vector2.MoveTowards(transform.position, dir.position, maxDistanceDelta);

        //waddle doo
        if(isDoo && (transform.position - GameObject.Find("Player").gameObject.GetComponent<Transform>().position).x < 15)
        {
            dooTimer += Time.deltaTime;
            if(dooTimer > 3f)
            {
                GetComponent<Animator>().runtimeAnimatorController = attackDoo;
                dooStop = true;
            }
            if(dooTimer > 4f && !spawnedHurt)
            {
                spawnedHurt = true;
                Instantiate(hurtObj, hurtSpawn);
               // Destroy(hurt, 2f);
            }
            if(dooTimer > 6f)
            {
                dooStop = false;
                spawnedHurt = false;
                dooTimer = 0f;
                GetComponent<Animator>().runtimeAnimatorController = defaultDoo;
            }
        }
        else
        {
            dooTimer = 0f;
        }
        
    }

    void Death()
    {
        Camera.main.GetComponent<AudioSource>().PlayOneShot(hitSoundCutter);
        GameObject deathObj = (GameObject)Instantiate(DeathEffect,transform);
        deathObj.transform.parent = null;
        Destroy(deathObj, 4f);
        PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 50);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Cutter" && !invincible)
        {
            Destroy(collision.gameObject);
            Death();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !block)
        {
            Vector2 kb = transform.position - collision.gameObject.GetComponent<Transform>().position;
            Vector2 knock;
            if (kb.x > 0)
            {
                knock = new Vector2(-1, 1);
            }
            else
            {
                knock = new Vector2(1, 1);
            }
            knock *= myEnemy.knockback;
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(1,knock);
        }
    }
}
