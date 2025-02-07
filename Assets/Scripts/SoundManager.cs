using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource BGMSource;
    [SerializeField] private AudioSource SESource;

    [SerializeField] private List<AudioClip> BGMs = new List<AudioClip>();
    [SerializeField] private List<AudioClip> SEs = new List<AudioClip>();

    [SerializeField]
    private int AreaBGMnum; 
    
    private void Start() {
        if(AreaBGMnum != -1){
            PlayBGM(AreaBGMnum);
        }
    }

    public void PlayBGM(int BGMnum){
        BGMSource.clip = BGMs[BGMnum];
        _playBGM();
    }

    public void PlayBGMWaitImage(int BGMnum){
        BGMSource.clip = BGMs[BGMnum];
        Invoke("_playBGM", 0.3f);
    }

    public void FadeOutBGM(float fadetime){
        StartCoroutine(_FadeOutBGM(fadetime));
    }

    public void PlaySE(int SEnum){
        SESource.PlayOneShot(SEs[SEnum]);
    }

    private void _playBGM(){
        BGMSource.Play();
    }

    private IEnumerator _FadeOutBGM(float fadeOutTime){
        float startVolume = BGMSource.volume;

        while (BGMSource.volume > 0)
        {
            BGMSource.volume -= startVolume * Time.deltaTime / fadeOutTime;
            yield return null;
        }

        BGMSource.Stop(); // フェードアウトが完了したらBGMを停止
        BGMSource.volume = startVolume; // ボリュームを元に戻す
    }
}
