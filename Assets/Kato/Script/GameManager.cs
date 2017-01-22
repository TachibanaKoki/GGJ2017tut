﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    private static GameManager mInstance;
    public  GameObject[] Character { get; private set; }
    private GameObject[] Rocks;
    bool isGameEnd;

    private GameManager()
    {
        // Private Constructor
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

    private void Awake()
    {
        Character = GameObject.FindGameObjectsWithTag("Player");
        Rocks = GameObject.FindGameObjectsWithTag("Rock");
        isGameEnd = false;
    }

    private void Update()
    {
        if (isGameEnd) return;

        int characterAlive = 0;
        bool RockAlive = false;
        for(int i =0;i < Character.Length;i++)
        {
            if(Character[i]!=null)
            {
                characterAlive++;
            }
        }

        for(int i=0;i< Rocks.Length;i++)
        {
            if (Rocks[i] != null)
            {
                RockAlive = true;
            }
        }

        if(characterAlive<1)
        {
            //  ドワーフがやられた
            isGameEnd = true;
            SceneManager.LoadSceneAsync("GameOver");
        }

        if(!RockAlive)
        {
            //  岩をすべて砕いた
            PlayerPrefs.SetInt("stage", PlayerPrefs.GetInt("stage") + 1);

            //  スコアの計算
            int score = characterAlive * 1000 + (int)( PlayerPrefs.GetFloat( "timer" ) * 100 ) + PlayerPrefs.GetInt("combo") * 100;
            PlayerPrefs.SetInt("score", score);
            isGameEnd = true;
            SceneManager.LoadSceneAsync("ResultScene");
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
