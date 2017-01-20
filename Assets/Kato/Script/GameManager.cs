using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private float endTime = 3.0f;
    private float nowTime = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        nowTime += Time.deltaTime;
        Debug.Log( "nowTime="+nowTime );
        if ( endTime < nowTime )
        {
            Debug.Log( "終わり" );
        }
    }
}
