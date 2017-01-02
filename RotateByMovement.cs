using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateByMovement : MonoBehaviour {

    private Transform myTransform;
    private PlayerMovement move;
    public float speedMultiplier;
    public float speed;

	// Use this for initialization
	void Start ()
    {
        myTransform = transform;
        move = GetComponentInParent<PlayerMovement>();
        speed = move.speed;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (move.horizontalAxis > 0 && !move.isAttatchedToWall)
        {
            myTransform.Rotate(0, 0, (move.horizontalAxis * Time.deltaTime * speed * speedMultiplier) * -1);
        }
        if (move.horizontalAxis < 0 && !move.isAttatchedToWall)
        {
            myTransform.Rotate(0, 0, (move.horizontalAxis * Time.deltaTime * speed * speedMultiplier));
        }
    }
}
