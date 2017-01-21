using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControle : MonoBehaviour {

    public float hp = 100;
    public bool flag = false;

	// Use this for initialization
	void Start () {
        flag = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (hp < 0) {
            Destroy(this.gameObject);
        }

        if (flag == true) {
            hp--;
        }
	}

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player") {
            flag = true;
        }
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            flag = false;
        }
    }
}
