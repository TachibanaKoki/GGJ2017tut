using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Temp : MonoBehaviour {

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

    #region フレーム処理

    // Use this for initialization
    void Start () {
        now -= delay;
        TapUtils.I.OnTapDown += TempoTest;
        TapUtils.I.OnTapDown += Combo;
        flag = false;
        combo = 0;
	}
	
	// Update is called once per frame
	void Update () {
        now++;
        Test();
        combonum.text = combo.ToString();
	}

    public void Combo(Vector3 vec) {
        if (flag == true)
        {
            combo++;
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
        }
        else
        {
            danger.color = new Color(0, 0, 0, 0);
        }
    }

    #endregion
}
