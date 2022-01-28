using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// BGM のボリュームを slider で変更
public class BGMSlider : MonoBehaviour
{
    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeBGMVolume()
    {
        AudioManager.Instance.SetBGMVolume(slider.value);
    }
}
