using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UFO が真上で爆発した時、または墜落した時に揺らすために作成
// 走る時などにも使えるかも
public class CameraShake : MonoBehaviour
{
    Coroutine shakeCoroutine;
    // [SerializeField] GameObject camera = null;

    void Update()
    {
        // if(Input.GetKeyDown(KeyCode.G))
        //     ShakeCamera(3.0f, 0.5f);
    }

    public void ShakeCameraExplosion()
    {
        ShakeCamera(0.3f, 0.5f);
    }


    // カメラを揺らすメソッド
    public void ShakeCamera(float duration, float magnitude)
    {
        shakeCoroutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    // 引数 カメラを揺らす時間と強さ
    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        // 元の位置
        Vector3 originalPos = transform.localPosition;
        float elapsed = 0;
        while( elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, originalPos.z);
            elapsed += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originalPos;
        StopCoroutine(shakeCoroutine);
        shakeCoroutine = null;
    }
}
