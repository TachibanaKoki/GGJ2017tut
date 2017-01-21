using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockControle : MonoBehaviour {

    public float hp = 100;
    public bool flag = false;

    public Sprite first, second, third;

    public float maxhp;

	// Use this for initialization
	void Start () {
        flag = false;
        maxhp = hp;
	}
	
	// Update is called once per frame
	void Update () {
        ChangeSprite();

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

    public void ChangeSprite() {
        if (hp < maxhp / 3)
        {
            this.GetComponent<SpriteRenderer>().sprite = third;
            return;
        }
        else if (hp > maxhp / 3 * 2)
        {
            this.GetComponent<SpriteRenderer>().sprite = first;
            return;
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = second;
            return;
        }
    }
}
