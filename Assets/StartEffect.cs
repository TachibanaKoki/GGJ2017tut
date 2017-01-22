using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartEffect : MonoBehaviour {

    float Timer;
    Text text;
    int count;

	// Use this for initialization
	void Start () {
        Timer = 0.0f;
        count = 3;
        text = GetComponent<Text>();
        text.text = count.ToString();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Timer += Time.deltaTime;
        text.fontSize = 200 + (int)(100 * Timer);
        text.color = new Color(text.color.r, text.color.g, text.color.b, Timer+0.2f);
        if(Timer>=1.0f)
        {
            Timer = 0.0f;
            count--;
            if (count <= 0)
            {
                Destroy(gameObject);
                return;
            }
            text.text = count.ToString();
        }
	}
}