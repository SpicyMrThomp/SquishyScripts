using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//To Use: Just change the "z rotation" to either 0 or 180

public class DropWhenAboveBelow : MonoBehaviour {

    public bool ready;
    public bool triggered;
    public bool playerTargeted;
    public float distance;
    public float speed;
    public float pointRadius;

    public bool dropping;
    public bool resetting;

    public Vector3 origin;
    public Vector3 translation;

    public Transform groundPoint;
    public Transform ceilingPoint;
    public LayerMask groundMask;
    public GameObject hazard;

    // Use this for initialization
    void Start ()
    {
        //rb = GetComponent<Rigidbody2D>();
        origin = transform.position;
        ready = true;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        hazard.transform.Translate(translation);

        if (playerTargeted && triggered && !resetting)
        {

            translation = Vector3.up * (speed * Time.deltaTime);
            dropping = true;
            resetting = false;
        }

        if (Physics2D.OverlapCircle(ceilingPoint.position, pointRadius, groundMask) && playerTargeted)
        {
            //playerAbove = false;
            dropping = false;
            resetting = true;
        }

        if (resetting && !dropping)
        {
            Reset();
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (!triggered)
            {
                distance = col.transform.position.y - this.transform.position.y;
                triggered = true;

                if (distance != 0)
                {
                    playerTargeted = true;
                }
            }
        }
    }

    void Reset()
    {
        if (playerTargeted)
        {
            //translation = new Vector3(origin.x, origin.y * ((speed / 4) * Time.deltaTime), origin.z);
            translation = Vector3.down * (speed * Time.deltaTime);
        }
        if (Physics2D.OverlapCircle(groundPoint.position, pointRadius, groundMask))
        {
            playerTargeted = false;
            dropping = false;
            resetting = false;
            triggered = false;
            ready = true;
            translation = Vector3.zero;
        }
    }

    //Draw Gizmos to help conceptualize collisions
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundPoint.position, pointRadius);
        Gizmos.DrawWireSphere(ceilingPoint.position, pointRadius);
    }
}
