using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class TapPlayAnimation : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    GameObject obj;

    //タップ
    public void OnPointerDown(PointerEventData eventData)
    {
        Animator anim = obj.GetComponent<Animator>();
        anim.Play("TapAnimation");
    }
}