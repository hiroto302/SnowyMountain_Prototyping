using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 各オブジェクトのSEを制御するスクリプトコンポーネント
[RequireComponent(typeof(AudioSource))]
public class SE : MonoBehaviour
{
    [SerializeField]
    AudioSource source = null;
    [SerializeField]
    AudioClip[] sound = null;

    void Reset()
    {
        source = GetComponent<AudioSource>();
        SetDefault();
    }


    // AudioSource のデフォルト設定
    void SetDefault()
    {
        source.priority = 128;
        source.volume = 0.3f;
        source.loop = false;
        source.spatialBlend = 1;
        source.playOnAwake = false;
    }

    // 指定した音を鳴らすメソッド
    public void PlaySE(int n)
    {
        source.PlayOneShot(sound[n], source.volume);
    }
    public void PlaySE(int n, float volume)
    {
        source.PlayOneShot(sound[n], volume);
    }
    public void PlaySE(int n, float volume, Vector3 position)
    {
        AudioSource.PlayClipAtPoint(sound[n], position, volume);
    }

    // ランダムな音を再生
    public void PlayRadomSE()
    {
        source.PlayOneShot(sound[Random.Range(0, sound.Length)]);
    }

}
