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

    [SerializeField]
    FadeUI fade;

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
            StartCoroutine(GotoScene());
        }
    }

    IEnumerator GotoScene()
    {
        //フェードの同期待ち
        var wait = fade.StartFadeIn();
        yield return wait;

        //    SceneManager.LoadScene("TitleScene");
        //  次のステージへ遷移（仮実装）
        switch ( PlayerPrefs.GetInt( "stage" ) )
        {
            case 1:
                SceneManager.LoadScene("Master");
                break;
            case 2:
                SceneManager.LoadScene("Master 1");
                break;
            case 3:
                SceneManager.LoadScene("Master 2");
                break;
            default:
                SceneManager.LoadScene("TitleScene");
                break;
        }
    }
}