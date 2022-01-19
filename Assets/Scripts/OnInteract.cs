using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


// このスクリプトコンポーネントは、Playerがシーン内のGameObjectとインタラクトしたときに発生するイベントを1つ以上指定する

// RayCastを利用してこのスクリプトをしたいため、collier をobject にアタッチすることを要求する
[RequireComponent(typeof(Collider))]
public class OnInteract : MonoBehaviour
{
    // イベントが一度だけ発生するか、ユーザーがGameObjectとインタラクトするたびに発生するか
    public bool isOneShot;
    // イベントが繰り返される場合、再び発生するまでの秒数
    public float cooldown;
    // このコンポーネントが追加されているGameObjectとユーザがインタラクトした時に発生するEvent
    public UnityEvent onInteractEvent;

    bool hasBeenTriggered;  // 既に実行されたか
    float timer;            // 実行されてからどれほど経過したか

    public Sprite pointer = null; // インタラクトされる時に表示するポインター


    void Start()
    {
        timer = cooldown;
    }

    void Update()
    {
        if(hasBeenTriggered)
            timer += Time.deltaTime;
    }

    // 登録されているメソッドを実行
    public void Interact()
    {
        if(isOneShot && hasBeenTriggered)
            return;

        if(cooldown > timer)
            return;

        onInteractEvent.Invoke();
        hasBeenTriggered = true;
        timer = 0;
    }
}
