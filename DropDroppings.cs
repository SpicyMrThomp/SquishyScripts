using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDroppings : MonoBehaviour {

    private Transform myTransform;
    public GameObject droppings;
    public float timer;
    public float currentTime;

	// Use this for initialization
	void Start ()
    {
        myTransform = transform;
        currentTime = timer;
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            GameObject freshDroppings = Instantiate(droppings, myTransform.position, Quaternion.identity) as GameObject;
            currentTime = timer;
        }
	}
}
