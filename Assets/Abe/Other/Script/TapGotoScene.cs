using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class TapGotoScene : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    float delayTime;

    [SerializeField]
    string sceneName;

    [SerializeField]
    LoadSceneMode mode;

    public void OnPointerDown(PointerEventData eventData)
    {
        StartCoroutine(GotoScene());
    }

    IEnumerator GotoScene()
    {
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneName, mode);
    }
}