using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinSaw : MonoBehaviour {

    private Transform myTransform;
    public int speed; //Multiplied by 100 later.

	// Use this for initialization
	void Start ()
    {
        speed = speed * 100;
        myTransform = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Rotate the object along the z axis. One degree at a time
        myTransform.Rotate(0, 0, speed * Time.deltaTime);
	}
}
