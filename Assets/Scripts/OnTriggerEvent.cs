using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
// このカスタムスクリプトコンポーネントは、
// トリガーとして設定されたコライダーによって定義されたエリアにユーザーが入ったり出たりしたときに起こる1つまたは複数のイベントを指定できる
// Is TriggerプロパティがそのColliderコンポーネントに対して有効になっている場合にのみ動作

[RequireComponent(typeof(Collider))]
public class OnTriggerEvent : MonoBehaviour
{
    // 侵入した時
    [Header("Trigger Enter Event Section")]
    public bool EnterIsOneShot;
    public float EnterEventCooldown;
    public UnityEvent OnTriggerEnterEvent;

    // 出た時
    [Header("Trigger Exit Event Section")]
    public bool ExitIsOneShot;
    public float ExitEventCooldown;
    public UnityEvent OnTriggerExitEvent;

    bool enterHasBeenTriggered;
    float enterTimer;

    bool exitHasBeenTriggered;
    float exitTimer;

    void Start()
    {
        enterTimer = EnterEventCooldown;
        exitTimer = ExitEventCooldown;
    }

    void Update()
    {
        if(enterHasBeenTriggered)
            enterTimer += Time.deltaTime;

        if(exitHasBeenTriggered)
            exitTimer += Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(EnterIsOneShot && enterHasBeenTriggered)
            return;

        if(EnterEventCooldown > enterTimer)
            return;

        OnTriggerEnterEvent.Invoke();
        enterHasBeenTriggered = true;
        exitTimer = 0f;
    }

    void OnTriggerExit(Collider other)
    {
        if(ExitIsOneShot && exitHasBeenTriggered)
            return;

        if(ExitEventCooldown > exitTimer)
            return;

        OnTriggerExitEvent.Invoke();
        exitHasBeenTriggered = true;
        exitTimer = 0f;
    }
}
