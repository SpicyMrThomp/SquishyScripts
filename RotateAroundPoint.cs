using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{

    private Transform myTransform;
    public GameObject rotation;
    public Transform rotationPoint;
    public float pointRadius;

    public bool counterClockWise;
    public bool clockWise;

    public int speed; //Multiplied by 100 later.

    // Use this for initialization
    void Start()
    {
        speed = speed * 100;
        rotationPoint = rotation.transform;
        myTransform = rotationPoint;

        if (counterClockWise)
        {
            speed = speed;
        }

        if (clockWise)
        {
            speed = speed * -1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Rotate the object along the z axis. One degree at a time
        myTransform.Rotate(0, 0, speed * Time.deltaTime);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(rotationPoint.position, pointRadius);
    }
}
