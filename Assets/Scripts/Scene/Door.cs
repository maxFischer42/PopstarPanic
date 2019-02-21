using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour {

    public string SceneName;
    public AudioClip door;
    private bool inDoor = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<Controller>().atDoor = true;
        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Input.GetAxisRaw("Vertical") > 0)
        {
            if (!inDoor)
            {
                inDoor = true;
                
                StartCoroutine(Wait(collision.gameObject));
            }            
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Controller>().atDoor = false;
        }
    }

    IEnumerator Wait(GameObject player)
    {
        player.GetComponent<AnimationController>().enabled = false;
        player.GetComponent<Controller>().enabled = false;
        Camera.main.GetComponent<AudioSource>().PlayOneShot(door);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneName);
    }
}
