using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFromPointToPoint : MonoBehaviour {
    
    public Path path;

    public Vector3 destination;
    Vector3 destinationDistance;
    public Vector3 currentPosition;

    public LayerMask pathPoint;
    public Transform centerPoint;
    public float pointRadius;

    public bool forward;
    public bool backward;
    public bool horizontal;
    public bool vertical;

    public bool triggered;

    public float distanceHorizontal;
    public float distanceVertical;
    public float speed;
    public int currentPathID;
    public float timer;
    public float currentTime;

	// Use this for initialization
	void Start ()
    {
        //currentDestination = transform.position;
        forward = true;
        currentPathID = 0;
        //CalculateDestinations();

        currentTime = timer;
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        TravelForward();

        currentPosition = transform.position;
        currentPosition = destination.normalized;
    }

    void TravelForward()
    {
        //Travel To Destinations
        if (Physics2D.OverlapCircle(centerPoint.position, pointRadius, pathPoint) && !triggered)
        {
            triggered = true;
            Debug.Log("Triggered");
            transform.Translate(Vector3.zero);
            //Timer();

            CalculateDestinations();
        }

        if (!Physics2D.OverlapCircle(centerPoint.position, pointRadius, pathPoint))
        {
            triggered = false;
        }

        distanceHorizontal = destination.x - transform.position.x;
        distanceVertical = destination.y - transform.position.y;
        transform.Translate(destinationDistance * (speed * Time.deltaTime), Space.World);
        //if (distanceHorizontal != 0 || distanceVertical != 0)
        //{

        //}
    }

    void CalculateDestinations()
    {
        if (currentPathID == (path.points.Length - 1))
        {
            currentPathID = 0;
            Debug.Log("Changed");
        }
        else
        {
            currentPathID++;
            Debug.Log("Changed");
        }

        destination = path.PointPosition(currentPathID);
        destinationDistance = destination - transform.position;
        destinationDistance = destinationDistance.normalized;
    }

    void Timer()
    {
        currentTime -= Time.deltaTime;
        do
        {
            
        } while (currentTime != 0);

        if (currentTime <= 0)
        {
            currentTime = timer;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(centerPoint.position, pointRadius);
        Gizmos.DrawLine(transform.position, path.PointPosition(currentPathID));
    }
}
