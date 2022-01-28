using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// audio を管理する manager クラス
// bgm の音量を option 画面を開いた時に bgm の音量を変更できるようにしたい
public class AudioManager : MonoSingletone<AudioManager>
{
    public AudioMixerSnapshot paused = null;
    public AudioMixerSnapshot unPaused = null;

    public AudioMixer masterMixer = null;

    // // dB -80 ~ 20 (0 の時、-0.08dB)
    public void SetBGMVolume(float volume)
    {
        masterMixer.SetFloat("BGMVolume", volume);
    }

    public void SetUnPausedSnapShot()
    {
        unPaused.TransitionTo(0f);
    }
    public void SetPausedSnapShot()
    {
        paused.TransitionTo(0f);
    }
}
