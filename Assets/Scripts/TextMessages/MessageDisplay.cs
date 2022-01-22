using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

// 送信されてきたメッセージを受信後、受信したメッセージを表示するクラス
// メッセージを表示するために, 非表示にしてある object を表示

public class MessageDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = null;
    [SerializeField] GameObject displayer = null;

    Coroutine fadeDisplayerCoroutine = null;
    // 表示し始める時に発生する event
    public static event Action OnDisplayMessage;
    // 表示が完了した時に発生する event
    public static event Action OnCompletDisplay;


    void Start()
    {
        if(text == null)
            text = GameObject.Find("Text Message").GetComponent<TextMeshProUGUI>();

        MessageReceiver.OnReceiveMessage += OnHandleReceiveMessage;
    }

    void OnHandleReceiveMessage(string message)
    {
        DisplayMessage(message);
    }

    // メッセージの表示
    public void DisplayMessage(string message)
    {
        // メッセージのfade 処理中でない時
        if(fadeDisplayerCoroutine == null)
        {
            OnDisplayMessage();
            displayer.SetActive(true);
            text.color = new Color(1, 1, 1, 1);
            text.text = message;
            fadeDisplayerCoroutine = StartCoroutine(FadeOutDisplayerRoutine(2.5f));
        }
    }

    // 一定時間後、表示した文字をfadeOutさせ、displayer を非表示にする
    IEnumerator FadeOutDisplayerRoutine(float second)
    {
        float r = text.color.r;         // text の色の初期値
        float g = text.color.g;
        float b = text.color.b;
        float elapsedTime = 0;          // 経過時間
        float alfa = 1;                 // 透明度の初期値
        float waitForSecond = 0.01f;    // 待機時間
        float fadeRate = 1.0f / second; // fade率 : 1秒間あたりの増加率
        yield return new WaitForSeconds(second);
        while(true)
        {
            elapsedTime += waitForSecond;
            if(elapsedTime >= second)
            {
                break;
            }
            yield return new WaitForSeconds(waitForSecond);
            alfa = 1.0f - fadeRate * elapsedTime;
            text.color = new Color(r, g, b, alfa);
        }
        StopCoroutine(fadeDisplayerCoroutine);
        fadeDisplayerCoroutine = null;
        displayer.SetActive(false);

        // 表示が終了したら発生するevent (次のメッセージがある場合,表示したいため)
        OnCompletDisplay();
    }
}
