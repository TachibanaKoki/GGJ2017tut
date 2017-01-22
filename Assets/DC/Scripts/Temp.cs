using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Temp : MonoBehaviour {

    public static Temp own;

    private bool k;

    public Image danger;
    public Text combonum;

    public float delay;
    public float tempo;
    public float min;
    public float max;

    private float start;
    private float now;

    public bool flag;

    public int combo;

    public int maxcombo = 99;

    private float nowtime;
    private bool tempotest;

    // Use this for initialization
    void Start () {
        //Initialize();
        own = this;
        danger.color = new Color(0, 0, 0, 0);

        Invoke("wait", 3);
        PlayerPrefs.GetInt("combo", 0);
	}
	
	// Update is called once per frame
	void Update () {

        if (k == true)
        {
            nowtime = SumTime();
            now = nowtime - start;
            Test();
        }

        combonum.text = "Combo : " + combo.ToString();

        //Debug.Log(tempotest);
	}

    public void wait() {
        TapUtils.I.OnTapDown += TempoTest;
        TapUtils.I.OnTapDown += Combo;
        k = true;
    }

    public void Initialize() {
        start = SumTime();
        start -= delay;
        flag = false;
        combo = 0;
        k = false;

        tempotest = false;
    }

    public void Combo(Vector3 vec) {
        if (flag == true)
        {
            if(combo < maxcombo)
            {
                combo++;
                if ( PlayerPrefs.GetInt("combo") < combo )
                {
                    PlayerPrefs.SetInt("combo", combo);
                }

                Sound.own.Play(Sound.own.se.whistl, 0);
            }
            
        }
        else {
            combo = 0;
        }
    }

    public void TempoTest(Vector3 vec) {
        //Debug.Log(now % tempo + max);

        if (now % tempo < Math.Abs(min) || tempo < now % tempo + max)
        {
            flag = true;
            //Sound.own.Play(Sound.own.se.whistl, 0);
        }
        else
        {
            flag = false;
        }
    }

    public void Test() {
        if (now % tempo < Math.Abs(min) || tempo < now % tempo + max)
        {
            danger.color = new Color(1, 0, 0, 0.2f);
            tempotest = true;
        }
        else
        {
            danger.color = new Color(0, 0, 0, 0);
            tempotest = false;
        }

        danger.color = new Color(0, 0, 0, 0);
    }

    public float SumTime() {
        float res = 0;

        //res += DateTime.Now.Day;
        //res *= 24;
        //res += DateTime.Now.Hour;
        //res *= 60;
        res += DateTime.Now.Minute;
        res *= 60;
        res += DateTime.Now.Second;
        res *= 1000;
        res += DateTime.Now.Millisecond;
        res = res / 1000 * 60;

        //Debug.Log(res);

        return res;
    }
}
