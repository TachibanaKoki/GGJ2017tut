using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class TitleAnimation : MonoBehaviour
{
    [SerializeField, Tooltip("動きの方向")]
    Vector2 movingDirection;

    [SerializeField, Tooltip("動きのスピード"), Range(0.0f, 50.0f)]
    float movingSpeed;
    public  float MovingSpeed
    {
        get { return movingSpeed; }
    }

    [SerializeField, Tooltip("色")]
    Gradient colors;

    [SerializeField, Tooltip("実際に生成するインスタンスのプレハブ")]
    GameObject createInstance;

    [SerializeField]
    float intervalTime;

    List<GameObject> obj = new List<GameObject>();

    void Awake()
    {
#if UNITY_EDITOR
        var result = createInstance.GetComponent<Image>();
        Debug.Assert(result != null, "Imageコンポーネントをつけてください");
#endif
    }
    
    IEnumerator Start()
    {
        while(true)
        {
            GameObject inst = Instantiate(createInstance, transform.position, Quaternion.identity);
            inst.transform.SetParent(transform);
            inst.transform.localScale = Vector3.one;
            obj.Add(inst);
            yield return new WaitForSeconds(intervalTime);
        }
    }
    
    void Update()
    {
        foreach(GameObject gameObj in obj)
        {
            if(gameObj == null)
            {
                obj.Remove(gameObj);
                return;
            }
            gameObj.transform.position += (Vector3)movingDirection * movingSpeed;

            //0から1へ変換
            Vector3 viewPos = Camera.main.WorldToViewportPoint(gameObj.transform.position);

            //グラデーション
            Image image = gameObj.GetComponent<Image>();
            image.color = colors.Evaluate(viewPos.x);
        }
    }
}