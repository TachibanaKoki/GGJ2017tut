using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour {

    [SerializeField]    ParticleSystem tapEffect;              // タップエフェクト
    [SerializeField]    Camera _camera;                        // カメラの座標

    [SerializeField]    bool playSound = false;
    AudioSource audioSource;

    // Use this for initialization
    void Start () {
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            // マウスのワールド座標までパーティクルを移動し、パーティクルエフェクトを1つ生成する
            var pos = _camera.ScreenToWorldPoint(Input.mousePosition + _camera.transform.forward * 10);
            tapEffect.transform.position = pos;
            tapEffect.Emit(1);
            if(playSound)
            {
                audioSource.Play();
            }
        }
    }
}
