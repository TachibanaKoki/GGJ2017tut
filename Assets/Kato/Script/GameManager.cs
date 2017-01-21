using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager mInstance;
    private GameObject[] Character;
    bool isGameEnd;
    

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

    private void Start()
    {
        Character = GameObject.FindGameObjectsWithTag("Player");
        isGameEnd = false;
    }

    private void Update()
    {
        if (isGameEnd) return;

        int characterAlive = 0;
        for(int i =0;i < Character.Length;i++)
        {
            if(Character[i]!=null)
            {
                characterAlive++;
            }
        }

        if(characterAlive<2)
        {
            SceneManager.LoadSceneAsync("GameOver");
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
