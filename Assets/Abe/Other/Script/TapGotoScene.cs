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

        //  次のステージへ遷移（仮実装）
        switch (PlayerPrefs.GetInt("stage"))
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

        if ( sceneName == "TitleScene" )
        {
            SceneManager.LoadScene(sceneName, mode);
        }

    }
}