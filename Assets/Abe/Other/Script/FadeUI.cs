using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class FadeUI : MonoBehaviour
{
    [SerializeField, Tooltip("説明文")]
    public float speed = 1;

    public float delay = 0.0f;

    //スクリプトのStartが呼ばれたときに
    enum StartFade
    {
        None,
        FadeIn,
        FadeOut
    }

    [SerializeField]
    StartFade startFade;

    Image image;

    void OnValidate()
    {
        if(speed < 0.0001f) speed = 0.0001f;
    }

    void Awake()
    {
        image = GetComponent<Image>();
    }

    void Start()
    {
        switch(startFade)
        {            
            case StartFade.FadeIn:  StartFadeIn();  break;
            case StartFade.FadeOut: StartFadeOut(); break;
        }
    }

    public IEnumerator StartFadeIn()
    {
        IEnumerator fadeIn = FadeIn();
        StartCoroutine(fadeIn);
        return fadeIn;
    }

    public IEnumerator StartFadeOut()
    {
        IEnumerator fadeOut = FadeOut();
        StartCoroutine(fadeOut);
        return fadeOut;
    }

    IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(delay);

        for(float t = 0.0f; t < 1; t += Time.deltaTime * speed)
        {
            SetAlpha(t);
            yield return null;
        }

        SetAlpha(1.0f);
    }

    void Update()
    {
        image.raycastTarget =  image.color.a != 0.0;
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(delay);

        for(float t = 1.0f; t > 0; t -= Time.deltaTime * speed)
        {
            SetAlpha(t);
            yield return null;
        }

        SetAlpha(0.0f);
    }

    void SetAlpha(float a)
    {
        Color color = image.color;
        color.a  = a;
        image.color = color;
    }
}