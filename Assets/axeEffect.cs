using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class axeEffect : MonoBehaviour {

    bool rotate = true;
    int z = 0;
    public Rigidbody2D rigid;
    public GameObject effect;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!rotate)
            return;
        transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0f, 0f, z));
        z += 5;
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            return;
        rigid.bodyType = RigidbodyType2D.Static;
        GameObject eff = (GameObject)Instantiate(effect, transform);
        Destroy(eff, 3.5f);
        rotate = false;
        Destroy(gameObject, 3.5f);
    }
}
