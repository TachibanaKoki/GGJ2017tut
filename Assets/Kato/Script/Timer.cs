using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameManager.Instance;
        gameManager.setTimer( 44.0f );  //  残り時間
    }
	
	// Update is called once per frame
	void Update () {

        float timer = gameManager.decreaseTimer();    //  時間経過

        Debug.Log("timer=" + timer);
        if ( timer <= 0 )
        {
            SceneManager.LoadScene("Abe/GameOver/GameOver");
        }

        //  雑ですが３秒対策
        if ( timer < 41.0f ) {
            this.GetComponent<Text>().text = "残りタイム:" + (int)timer;
            PlayerPrefs.SetFloat("timer", timer);
        }

    }
}
