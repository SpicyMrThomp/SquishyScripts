using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDoor : MonoBehaviour {

    public GameObject door;
    public Keys keys;

    void Update()
    {
        if (!keys.locked)
        {
            Destroy(door);
        }
    }
}
