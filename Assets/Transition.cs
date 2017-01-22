using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour {

    Image image;
	// Use this for initialization
	void Start () {
        image = GetComponent<Image>();
	}

    IEnumerator FadeOut(Image image)
    {
        image.color = Color.clear;
        float t = 0.0f;
        float d  = 0.5f;
        while(true)
        {
            t += Time.deltaTime;
            image.color = Color.black * (t/d);
            yield return null;
            if(t>d)
            {
                image.color = Color.black;
                break;
            }
        }
    }
    IEnumerator FadeIn()
    {
        float t = 0.0f;
        float d = 1.0f;
        while (true)
        {
            yield return null;
            if (t > d)
            {
                break;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
