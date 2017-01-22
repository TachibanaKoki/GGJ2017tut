using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SE{
    public AudioClip whistl;
    public AudioClip Dig2;
};

[System.Serializable]
public class BGM
{
    public AudioClip stage;
};

public class Sound : MonoBehaviour {

    public AudioSource BgmSource;
    public AudioSource SeSource;
    
    public BGM bgm;
    
    public SE se;

    private AudioClip c;

    public static Sound own;

    private Temp tem;

    public void Play(AudioClip audio, float wait) {
        c = audio;

        Invoke("PlaySE", wait);
    }

    private void PlaySE() {
        SeSource.PlayOneShot(c);
    }

    public void SetBGM(AudioClip a) {
        BgmSource.clip = a;
        BgmSource.loop = true;
        BgmSource.Play();
        tem.Initialize();
    }

    public void Awake() {
        own = this;
    }

    public void Start() {
        own = this.GetComponent<Sound>();
        tem = Camera.main.GetComponent<Temp>();
        SetBGM(bgm.stage);
    }
}
