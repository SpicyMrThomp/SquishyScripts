using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour {

    public GameObject player;
    public float timer;
    //public GameObject[] hazardArray;

	// Use this for initialization
	void Start ()
    {
        GeneratePlayer();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (!GameObject.FindGameObjectWithTag("Player"))
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                ResetHazards();
            }
        }
	}

    void GeneratePlayer()
    {
        GameObject newPlayer = Instantiate(player, transform.position, Quaternion.identity) as GameObject;
        newPlayer.transform.parent = transform;
    }

    void ResetHazards()
    {
        int scene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(scene, LoadSceneMode.Single);
    }
}
