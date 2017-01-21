using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class ShowResultScore : MonoBehaviour
{
    [SerializeField, Tooltip("スコアを表示させるテキスト")]
    Text score;

    [SerializeField, Tooltip("スコアの最大桁数")]
    int  maxDigit;

    [SerializeField, Tooltip("時間")]
    float time;

    [SerializeField]
    public int resultScore;

    public bool isAnimationPlay;

    IEnumerator showScore;
    public IEnumerator WaitForShowScore
    {
        get { return showScore; }
    }

    void Start()
    {
        showScore = ShowScore();
        StartCoroutine(showScore);
        resultScore = PlayerPrefs.GetInt("score");
    }

    IEnumerator ShowScore()
    {
        isAnimationPlay = true;
        for(float t = 0.0f; t < time; t += Time.deltaTime)
        {
            if(!isAnimationPlay)
            {
                yield break;
            }
            int i = Random.Range(0, (int)Mathf.Pow(10, maxDigit));
            //maxDigitが1の場合は0-9がかえる
            string text = i.ToString("d" + maxDigit.ToString());

            score.text  = text;

            yield return null;
        }

        //最終スコア
        Show();

        isAnimationPlay = false;
    }

    public void Show()
    {
        score.text = resultScore.ToString("D" + maxDigit.ToString());
    }

    public void StopShowAnim()
    {
        isAnimationPlay = false;
        Show();
    }
}