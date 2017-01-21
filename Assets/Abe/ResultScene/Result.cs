using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class Result : MonoBehaviour, IPointerDownHandler
{
    [SerializeField, Tooltip("説明文")]
    ShowResultScore showScore;

    [SerializeField]
    GameObject returnTitle;

    void Awake()
    {
        returnTitle.SetActive(false);
    }
    
    IEnumerator Start()
    {
        //コルーチンが実行される前に実行されるのを防ぐため
        yield return null;

        yield return showScore.WaitForShowScore;
        returnTitle.SetActive(true);
        GetComponent<Animator>().Play("Flash");
    }
    
    void Update()
    {
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(showScore.isAnimationPlay)
        {
            showScore.StopShowAnim();
        }
        else
        {
            SceneManager.LoadScene("TitleScene");
        }
    }
}