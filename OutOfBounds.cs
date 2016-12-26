using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour {

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
            Destroy(col.gameObject);
    }
}
