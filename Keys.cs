using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keys : MonoBehaviour {

    public List<GameObject> keys;
    public bool locked;
    public int count;

    void Start()
    {
        locked = true;
        count = 0;
    }

    void Update()
    {
        if (count == keys.Count)
        {
            locked = false;
        }
    }
}
