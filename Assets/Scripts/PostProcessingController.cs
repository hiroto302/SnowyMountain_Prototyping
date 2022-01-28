using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


// 動的に PostProcessing の GlobalVolume を変更する
// GlobalVolume を変更するならふた通り考えられる。 volume.profile を代入して切り替える or Priority を利用する
// 今回は、Priority で切り替える
// 花火が上がっって発火した瞬間
// 発光を表現する profile を持つオブジェクトにアタッチすることを想定

public class PostProcessingController : MonoBehaviour
{
    [SerializeField] private Volume volume;
    [SerializeField] GameObject FlashGrobalVolumeObject = null;
    Coroutine flashCoroutine = null;
    private Bloom bloom;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H))
        {
            Flash();
        }
    }

    // 閃光のGrobalVolume を使用するか
    public void SetFlashGrobalVolume(bool flash)
    {
        FlashGrobalVolumeObject.SetActive(flash);
    }

    // 閃光を再現する
    public void Flash()
    {
        flashCoroutine = StartCoroutine(FlashRoutine());
    }
    IEnumerator FlashRoutine()
    {
        volume.profile.TryGet<Bloom>(out bloom);
        // 初期値
        float initialIntensity = 2.0f;
        float initialClamp = 4.0f;
        float initialThreshold = 1.5f;
        // 閃光した時の最大値
        float maxIntensity = 30.0f;
        float maxClamp = 100.0f;
        // 最初値
        float minThreshold = 1.0f;

        float elapsedTime = 0;
        float waitForSecond = 0.01f;
        float arrivalTime = 0.3f;           // 最大値に到達する時間
        // fade率
        float intensityFadeInRate = (maxIntensity - initialIntensity) / arrivalTime;
        float clampFadeInRate = (maxClamp - initialClamp) / arrivalTime;
        float thresholdFadeOutRate = (initialIntensity - minThreshold) / arrivalTime;

        float interval = 0.5f;

        // 閃光開始
        while(true)
        {
            elapsedTime += waitForSecond;
            if(elapsedTime >= arrivalTime)
            {
                break;
            }
            yield return new WaitForSeconds(waitForSecond);
            bloom.intensity.value = initialIntensity + intensityFadeInRate * elapsedTime;
            bloom.clamp.value = initialClamp + clampFadeInRate * elapsedTime;
            bloom.threshold.value = initialClamp - thresholdFadeOutRate * elapsedTime;
        }
        bloom.intensity.value = maxIntensity;
        bloom.clamp.value = maxClamp;
        bloom.threshold.value = minThreshold;
        yield return new WaitForSeconds(interval);

        // 初期値
        elapsedTime = 0f;
        arrivalTime = 0.7f;     // 初期値に到達するまでの時間
        float intensityFadeOutRate = (maxIntensity - initialIntensity) / arrivalTime;
        float clampFadeOutRate = (maxClamp - initialClamp) / arrivalTime;
        float thresholdFadeInRate = (initialThreshold - minThreshold) / arrivalTime;
        // 閃光の減衰
        while(true)
        {
            elapsedTime += waitForSecond;
            if(elapsedTime >= arrivalTime)
            {
                break;
            }
            yield return new WaitForSeconds(waitForSecond);
            bloom.intensity.value = maxIntensity - intensityFadeOutRate * elapsedTime;
            bloom.clamp.value = maxClamp - clampFadeOutRate * elapsedTime;
            bloom.threshold.value = minThreshold + thresholdFadeInRate * elapsedTime;
        }
        StopCoroutine(flashCoroutine);
        flashCoroutine = null;
    }

    void ChangeProfile()
    {
        // volume.profile = flashProfile;
    }
}
