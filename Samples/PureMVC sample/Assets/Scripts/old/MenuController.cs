using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBtnStartClick()
    {
        Debug.Log("Start");
        SceneManager.LoadScene("scene1", LoadSceneMode.Single);
    }

    public void OnBtnScoreClick()
    {
        Debug.Log("Score");
    }

    public void OnBtnExitClick()
    {
        Debug.Log("Exit");
    }

    public void OnBtnGameOver()
    {
        SceneManager.LoadScene("menu", LoadSceneMode.Single);
    }

}
