using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// // このカスタムスクリプトコンポーネントは、
// トリガーとして設定されたコライダーによって定義されたエリアにユーザーが入ったり出たりしたときに起こる1つまたは複数のイベントを指定できる
// Is TriggerプロパティがそのColliderコンポーネントに対して有効になっている場合にのみ動作
// event を毎回 or 一度 or 時間間隔で呼ぶことが設定できる
[RequireComponent(typeof(Collider))]
public class OnTriggerEvent : MonoBehaviour
{
    [Header("Trigger Enter Event Section")]
    [SerializeField] bool enterIsOneShot;       // 一度実行するのみか
    [SerializeField] float enterEventCooldown;  // 次に実行可能にするまでの時間
    [SerializeField] UnityEvent onTriggerEnterEvent = null;

    [Space]
    [Header("Trigger Exit Event Section")]
    [SerializeField] bool exitIsOneShot;
    [SerializeField] float exitEventCooldown;
    [SerializeField] UnityEvent onTriggerExitEvent = null;

    bool enterHasBeenTrigged;                   // 既に実行したか
    float enterTimer;                           // 侵入してからの経過時間
    Coroutine enterTimerCoroutine;
    bool exitHasBeenTrigged;
    float exitTimer;
    Coroutine exitTimerCoroutine;

    void Start()
    {
        // 最初の１度目を実行できるようにするため
        enterTimer = enterEventCooldown;
        exitTimer = exitEventCooldown;
    }


    // 侵入した時
    void OnTriggerEnter(Collider other)
    {
        if(onTriggerEnterEvent == null)
            return;

        if(enterIsOneShot && enterHasBeenTrigged)
            return;

        if(enterEventCooldown > enterTimer)
            return;

        onTriggerEnterEvent.Invoke();
        enterHasBeenTrigged = true;

        if(enterIsOneShot == false && enterTimerCoroutine == null)
            enterTimerCoroutine = StartCoroutine(CountEnterCoolDownTimeRoutine());
    }

    // 出た時
    void OnTriggerExit(Collider other)
    {
        if(onTriggerExitEvent == null)
            return;

        if(exitIsOneShot && exitHasBeenTrigged)
            return;

        if(exitEventCooldown > exitTimer)
            return;

        onTriggerExitEvent.Invoke();
        exitHasBeenTrigged = true;

        if(exitIsOneShot == false && exitTimerCoroutine == null)
            exitTimerCoroutine = StartCoroutine(CountExitCoolDownTimeRoutine());

    }

    // クールダウンを計測するルーティーン
    IEnumerator CountEnterCoolDownTimeRoutine()
    {
        // 初期化
        enterTimer = 0;
        float waitTime = 0.1f;
        while(enterEventCooldown > enterTimer)
        {
            enterTimer += waitTime;
            yield return new WaitForSeconds(waitTime);
        }
        StopCoroutine(enterTimerCoroutine);
        enterTimerCoroutine = null;
    }
    IEnumerator CountExitCoolDownTimeRoutine()
    {
        exitTimer = 0;
        float waitTime = 0.1f;
        while(exitEventCooldown > exitTimer)
        {
            exitTimer += waitTime;
            yield return new WaitForSeconds(waitTime);
        }
        StopCoroutine(exitTimerCoroutine);
        exitTimerCoroutine = null;
    }
}
