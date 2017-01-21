using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager mInstance;

    private GameManager()
    { // Private Constructor

        Debug.Log("Create Singleton instance.");
    }


    public static GameManager Instance
    {
        get
        {
            if (mInstance == null) mInstance = new GameManager();
            return mInstance;
        }
    }

    //  スコア
    private int score;
    public void setScore(int score)
    {
        this.score = score;
    }
    public float getScore()
    {
        return score;
    }

    //  残り時間
    private float timer;
    public void setTimer(float timer)
    {
        this.timer = timer;
    }
    public float getTimer()
    {
        return timer;
    }
    public float decreaseTimer()
    {
        timer -= Time.deltaTime;
        return timer;
    }

}
