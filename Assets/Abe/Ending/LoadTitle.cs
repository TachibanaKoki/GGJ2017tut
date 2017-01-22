using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadTitle : MonoBehaviour {

    public Text tex;
	// Use this for initialization
	void Start () {
        tex.enabled = false;
        StartCoroutine(texenable());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator texenable()
    {
        yield return new WaitForSeconds(2.0f);
        tex.enabled = true;
    }

    public void LoadT(string name)
    {
        SceneManager.LoadScene(name);
    }
}
