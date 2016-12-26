using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour {

    public Transform[] points;

    public Vector3 PointPosition(int id)
    {
        return points[id].position;
    }
}
