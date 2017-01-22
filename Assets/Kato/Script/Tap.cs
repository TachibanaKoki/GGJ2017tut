using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{

    [SerializeField]
    ParticleSystem tapEffect;              // タップエフェクト
    [SerializeField]
    Camera _camera;                        // カメラの座標

    [SerializeField]
    bool playSound = false;

    [SerializeField]
    ParticleSystem GrateEffect;
    [SerializeField]
    ParticleSystem MissEffect;
    AudioSource audioSource;

    private Camera m_MainCamera;
    private Temp m_temp;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        try
        {
            m_temp = Camera.main.gameObject.GetComponent<Temp>();
        }
        catch
        {
            m_temp = null;
            Debug.Log("nottemp");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Vector3.zero;
            // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
            pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            if (m_temp == null)
            {
                tapEffect.transform.position = pos;
                tapEffect.Emit(1);
            }
            else
            {
                if(m_temp.timing>0.5f)
                {
                    GrateEffect.transform.position = pos;
                    GrateEffect.Emit(1);
                }
                else
                {
                    tapEffect.transform.position = pos;
                    tapEffect.Emit(1);
                }
            }

            if (playSound)
            {
                audioSource.Play();
            }
        }
    }
}
