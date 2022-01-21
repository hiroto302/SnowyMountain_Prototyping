using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// Image を fade させるクラス
// fade out : 透明度を徐々に減少(1 黒  => 0透明)
// fade in  : 透明度を徐々に増加(0 透明 => 1黒)
public class FadeImage : MonoBehaviour
{
    Coroutine fadeImageCoroutine;
    // 制御する Image
    [SerializeField] Image image = null;
    // Image の初期値を習得
    float red, green, blue, alfa;

    void Start()
    {
        GetInitialColor();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            FadeIn(3.0f);
        }
        if(Input.GetKeyDown(KeyCode.V))
        {
            FadeOut(3.0f);
        }
    }

    public void FadeIn(float second)
    {
        fadeImageCoroutine = StartCoroutine(FadeInImageRoutine(second));
    }

    public void FadeOut(float second)
    {
        fadeImageCoroutine = StartCoroutine(FadeOutImageRoutine(second));
    }

    // fade In して  fade Out するメソッド
    public void FadeInToOut(float second, float duration, Action action = null)
    {
        fadeImageCoroutine = StartCoroutine(FadeInToOutImageRoutine(second, duration, action));
    }

    // alfa 0 => 1 に second の時間をかけて増加させていく
    IEnumerator FadeInImageRoutine(float second)
    {
        float elapsedTime = 0;          // 経過時間
        alfa = 0;                       // 透明度の初期値
        float waitForSecond = 0.05f;     // 待機時間
        float fadeRate = 1.0f / second;  // fade率 : 1秒間あたりの増加率
        while(true)
        {
            elapsedTime += waitForSecond;
            if(elapsedTime >= second)
            {
                break;
            }
            yield return new WaitForSeconds(waitForSecond);
            alfa = fadeRate * elapsedTime;
            image.color = new Color(red, green, blue, alfa);
        }
        image.color = new Color(red, green, blue, 1.0f);
        StopCoroutine(fadeImageCoroutine);
        fadeImageCoroutine = null;
    }
    // alfa 1 => 0
    IEnumerator FadeOutImageRoutine(float second)
    {
        float elapsedTime = 0;          // 経過時間
        alfa = 1;                       // 透明度の初期値
        float waitForSecond = 0.05f;     // 待機時間
        float fadeRate = 1.0f / second;  // fade率 : 1秒間あたりの増加率
        while(true)
        {
            elapsedTime += waitForSecond;
            if(elapsedTime >= second)
            {
                break;
            }
            yield return new WaitForSeconds(waitForSecond);
            alfa = 1.0f - fadeRate * elapsedTime;
            image.color = new Color(red, green, blue, alfa);
        }
        image.color = new Color(red, green, blue, 0);
        StopCoroutine(fadeImageCoroutine);
        fadeImageCoroutine = null;
    }

    IEnumerator FadeInToOutImageRoutine(float second, float duration, Action action = null)
    {
        float elapsedTime = 0;          // 経過時間
        alfa = 0;                       // 透明度の初期値
        float waitForSecond = 0.05f;     // 待機時間
        float fadeRate = 1.0f / second;  // fade率 : 1秒間あたりの増加率
        while(true)
        {
            elapsedTime += waitForSecond;
            if(elapsedTime >= second)
            {
                break;
            }
            yield return new WaitForSeconds(waitForSecond);
            alfa = fadeRate * elapsedTime;
            image.color = new Color(red, green, blue, alfa);
        }
        alfa = 1.0f;
        image.color = new Color(red, green, blue, alfa);
        if(action != null)
            action();
        yield return new WaitForSeconds(duration);
        elapsedTime = 0;
        while(true)
        {
            elapsedTime += waitForSecond;
            if(elapsedTime >= second)
            {
                break;
            }
            yield return new WaitForSeconds(waitForSecond);
            alfa = 1.0f - fadeRate * elapsedTime;
            image.color = new Color(red, green, blue, alfa);
        }
        image.color = new Color(red, green, blue, 0);
        StopCoroutine(fadeImageCoroutine);
        fadeImageCoroutine = null;
    }

    void GetInitialColor()
    {
        red = image.color.r;
        green = image.color.g;
        blue = image.color.b;
        alfa = image.color.a;
    }
}
