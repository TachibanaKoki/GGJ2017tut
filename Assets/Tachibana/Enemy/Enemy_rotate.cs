using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_rotate : MonoBehaviour
{
    float rotateAngle;
    int rotateSpeed;

    // Use this for initialization
    void Start () {
        rotateAngle = 0.0f;
        rotateSpeed = 150;
	}
	
	// Update is called once per frame
	void Update () {
        rotateAngle += Time.deltaTime * rotateSpeed;
        transform.rotation = Quaternion.Euler(0, 0, rotateAngle);
    }
}
