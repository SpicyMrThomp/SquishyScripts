using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    public Text timeText;
    private float time = 0.0f;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        //Calculates the time
        time += Time.deltaTime;
        timeText.text = time.ToString("F2");
	}
}
