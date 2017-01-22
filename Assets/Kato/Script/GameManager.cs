using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private static GameManager mInstance;
    public  GameObject[] Character { get; private set; }
    private GameObject[] Rocks;
    bool isGameEnd;

    private Image fadePanel;
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
        fadePanel = GameObject.Find("Panel").GetComponent<Image>();
        fadePanel.gameObject.SetActive(false);
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
            StartCoroutine(FadeOut(fadePanel));
            StartCoroutine(Delay(1.0f, () => {
                SceneManager.LoadSceneAsync("GameOver");
            }));
        }

        if(!RockAlive)
        {
            //  岩をすべて砕いた
            PlayerPrefs.SetInt("stage", PlayerPrefs.GetInt("stage") + 1);

            //  スコアの計算
            int score = characterAlive * 1000 + (int)( PlayerPrefs.GetFloat( "timer" ) * 100 ) + PlayerPrefs.GetInt("combo") * 100;
            PlayerPrefs.SetInt("score", score);
            isGameEnd = true;
            StartCoroutine(FadeOut(fadePanel));
            StartCoroutine(Delay(1.0f,() => { SceneManager.LoadSceneAsync("ResultScene"); }));
        }

    }
    IEnumerator FadeOut(Image image,Action callback=null)
    {
        image.color = Color.clear;
        image.gameObject.SetActive(true);
        float t = 0.0f;
        float d = 1.0f;
        while (true)
        {
            t += Time.deltaTime;
            image.color = Color.black * (t / d);
            yield return null;
            if (t > d)
            {
                image.color = Color.black;
                break;
            }
        }
        if(callback!=null)
        callback();
    }
    IEnumerator Delay(float duration,System.Action action)
    {
        yield return new WaitForSeconds(duration);
        action();
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
