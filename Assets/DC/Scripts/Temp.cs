using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour {

    public float tempo = 100;
    public float min = -50;
    public float max = 50;

    private float firsttime = 0;
    private float time = 0;
    private float lasttap = 0;
    private float tap = 0;

	// Use this for initialization
	void Start () {
        TapUtils.I.OnTapDown += GetTempo;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetTempo(Vector3 vec) {
        Debug.Log("a");
    }
}
