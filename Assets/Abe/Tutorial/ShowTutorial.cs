using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ShowTutorial : MonoBehaviour, IPointerDownHandler
{
    [System.Serializable]
    struct Tutorial
    {
        [Header("スプライトが切り替わった時に再生するアニメーション名")]
        public string startAnimationName;
        [Header("タップ時に再生するアニメーション名")]
        public string tapAnimationName;
        [Header("表示するスプライト名")]
        public Sprite showSprite;
    }

    [SerializeField, Tooltip("上から順番に実行")]
    Tutorial[] tutorials;

    [SerializeField, Tooltip("表示用のイメージコンポーネント")]
    Image image;

    [SerializeField]
    Animator animator;

    IEnumerator show;

    void Awake()
    {
        
    }
    
    void Start()
    {
        show = Show();
        StartCoroutine(show);
    }
    
    IEnumerator Show()
    {
        foreach(Tutorial show in tutorials)
        {
            image.sprite = show.showSprite;
            yield return new WaitForSeconds(0.3f);

            WaitForTap();
            yield return null;

            //アニメーションの再生
            if(show.tapAnimationName != "")
            {
                animator.Play(show.tapAnimationName);
                yield return new WaitForSeconds(5.0f);
            }
        }

        //シーン移動可能にする
        image.raycastTarget = false;
    }

    void WaitForTap()
    {
        StopCoroutine(show);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(show);
    }
}