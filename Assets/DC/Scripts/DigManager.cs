using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigManager : MonoBehaviour {

    #region 変数格納

    public MapManager man;
    public GameObject ground;
    public GameObject fossil;

    public float tension = 1;

    public float score = 100;
    public float damagerate = 20;
    public float minus = 50;

    public float dig = 0;
    public float digmax = 100;

    public int x, y;

    public float a;

    private float alpha = 0;

    #endregion

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Dig(dig);
        man.score[x, y] = ScoreMus();
        a = ScoreMus();
	}

    public void Dig(float a) {
        alpha = a * tension / 100;
        fossil.GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, alpha);
    }

    public float ScoreMus() {
        float res = 0;

        if (dig >= digmax) {
            res = score;
            man.map[x, y] = true;
            if (dig > digmax + damagerate) {
                res -= Mathf.Floor((dig - digmax) / damagerate) * minus;
            }
        }

        if (res < 0) {
            res = 0;
            man.map[x, y] = false;
        }

        return res;
    }
}
