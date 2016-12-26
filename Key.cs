using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public Keys keys;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            keys.count++;
            Destroy(this.gameObject);
        }
    }
}
