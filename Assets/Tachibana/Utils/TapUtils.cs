using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TapUtils : MonoBehaviour
{
    public static TapUtils I;
    public delegate void TapEvent(Vector3 pos);
    public TapEvent OnTapDown;

    void Awake()
    {
        I = this;
    }

	void Update ()
    {
        if(Input.GetMouseButtonDown(0))
        {
            OnTapDown.Invoke(Input.mousePosition);
        }
	}
}
