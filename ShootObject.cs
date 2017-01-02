using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour {

    public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
	}

    void OnColliderStay2D(Collider2D col)
    {
        Debug.Log("OnColliderStay");
        if (col.tag == "Ground")
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        Debug.Log("OnTriggerStay");
        if (col.tag == "Pool")
        {
            Destroy(this.gameObject);
        }
    }
}
