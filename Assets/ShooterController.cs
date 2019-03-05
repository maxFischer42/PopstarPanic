using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour {

    public float maxDistanceDelta = 1f;
    public float projspeed;
    public GameObject proj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position + moveInput(), maxDistanceDelta);
        if(Input.GetButtonDown("Attack"))
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        GameObject projectile = (GameObject)Instantiate(proj, transform);
        projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(projspeed, 0f);
        projectile.transform.parent = null;
        Destroy(projectile, 7f);
    }

    public Vector3 moveInput()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y);
    }
}
