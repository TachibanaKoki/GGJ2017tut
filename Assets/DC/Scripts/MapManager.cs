using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Set {
    public int x, y;
};

[System.Serializable]
public class Pare {
    public Set[] set;
};

public class MapManager : MonoBehaviour {

    public int mapx = 3;
    public int mapy = 3;
    public bool[,] map;
    public float[,] score;

    public Pare[] pare;

    private int num;

    public float sum;

	// Use this for initialization
	void Start () {
        map = new bool[mapx, mapy];
        score = new float[mapx, mapy];
        sum = 0;
	}
	
	// Update is called once per frame
	void Update () {
        sum = 0;

        for (int i = 0; i < mapx; i++) {
            for (int n = 0; n < mapy; n++) {
                sum += score[i, n];
            }
        }

        CompareSet();
	}

    public void CompareSet() {
        num = 0;

        for (int i = 0; i < pare.Length; i++) {

            num = 0;

            for (int n = 0; n < pare[i].set.Length; n++) {
                if (map[pare[i].set[n].x, pare[i].set[n].y] == true) {
                    num++;
                }
            }

            //Debug.Log(num);
            //Debug.Log(pare[i].set.Length);

            if (num == pare[i].set.Length) {
                Complete();
            }
        }
    }

    public void Complete() {
        sum = sum * 2;
    }
}