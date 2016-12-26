using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    private Transform target;

    public float boundsTop = 0f;
    public float boundsBottom = 0f;
    public float boundsLeft = 0f;
    public float boundsRight = 0f;

    // Use this for initialization
    void Start () {
        //target = GameObject.FindWithTag("Player").transform;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        target = GameObject.FindWithTag("Player").transform;
        //Mathf.Clamp limits how far the camera can move by creating upper and lower bounds
        transform.position = new Vector3(Mathf.Clamp(target.position.x, boundsLeft, boundsRight), 
                                        Mathf.Clamp(target.position.y, boundsBottom, boundsTop), 
                                        transform.position.z);
    }
}
