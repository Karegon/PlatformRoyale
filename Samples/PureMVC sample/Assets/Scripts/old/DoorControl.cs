using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            string sceneName;
            if (SceneManager.GetActiveScene().name == "scene1")
                sceneName = "scene2";
            else
                sceneName = "gameEnd";

            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }


    }

}
