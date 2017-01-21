using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("stage", 1);
    }
	
	// Update is called once per frame
	void Update () {
    //    SceneManager.LoadScene("Abe/TitleScene/TitleScene");
    }
}
