using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameManager.Instance;
        gameManager.setScore(0);
    }
	
	// Update is called once per frame
	void Update () {
        this.GetComponent<Text>().text = "スコア:" + gameManager.getScore();
    }
}
