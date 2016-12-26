using UnityEngine;
using System.Collections;

public class DeathOnTouch : MonoBehaviour {

    public bool isPool = false;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            
            Destroy(col.gameObject);
        }

        if (isPool && col.tag == "Goo Ball")
        {
            Destroy(col.gameObject);
        }
    }
}
