using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
#if UNITY_EDITOR
using UnityEditor;
#endif

// Audio Mixer を利用してシーン全体の音を制御するサンプル
// Snapshots を利用して、音の切り替えを試す
// volume の音量切り替えを試す
public class SoundManger_sample : MonoBehaviour
{
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot unPaused;

    public AudioMixer masterMixer;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

        if(Input.GetKeyDown(KeyCode.V))
        {
            SetBGMVolume(0.5f);
        }
    }

    public void Pause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        Lowpass();
    }

    void Lowpass()
    {
        if(Time.timeScale == 0)
        {
            paused.TransitionTo(0f);
        }
        else
        {
            unPaused.TransitionTo(0f);
        }
    }

    // dB -80 ~ 20 (0 の時、-0.08dB)
    public void SetBGMVolume(float dB)
    {
        masterMixer.SetFloat("BGMVolume", dB);
    }


    void Quite()
    {
        #if UNITY_EDITOR
        EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
