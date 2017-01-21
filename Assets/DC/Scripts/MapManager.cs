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

	// Use this for initialization
	void Start () {
        map = new bool[mapx, mapy];
        score = new float[mapx, mapy];
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CompareSet() {

    }
}