using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByMovement : MonoBehaviour {

    private Transform myTransform;
    public float speedMultiplier;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        myTransform = transform;
        speed = myTransform.parent.GetComponentInParent<PlayerMovement>().speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            myTransform.Rotate(0, 0, (Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed * speedMultiplier) * -1);
        }
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            myTransform.Rotate(0, 0, (Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed * speedMultiplier));
        }
    }
}
