using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BGM制御スクリプト Default設定を記述
// BGM を担当するオブジェクトにAudioSourceをアタッチ
public class BGMController  : MonoBehaviour
{
    [SerializeField]
    protected AudioSource audioSource = null;
    // BGM
    [SerializeField]
    protected AudioClip bgm = null;
    void Reset()
    {
        audioSource = GetComponent<AudioSource>();
        // BGMのセット
        if(audioSource.clip == null)
        {
            audioSource.clip = bgm;
        }
        SetDefault();
    }
    void Awake()
    {
        // BGMのセット
        if(audioSource.clip == null)
        {
            audioSource.clip = bgm;
            // 音の再生
            audioSource.Play();
        }
    }

    void SetDefault()
    {
        // AudioSource の 各種設定
        // 優先度 最優先
        audioSource.priority = 0;
        // 音量
        audioSource.volume = 0.25f;
        // ループ
        audioSource.loop = true;
        // 音の2D化
        audioSource.spatialBlend = 0;
        // 音の再生タイミング
        audioSource.playOnAwake = true;
    }
}
