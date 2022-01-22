using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 送られてくるメッセージを受け取るクラス
// 複数のメッセージが送られてきたら順番に表示できるように、ここで溜めていくのもあり
public class MessageReceiver : MonoBehaviour
{
    // メッセージを受信している時の event
    public static event Action<string> OnReceiveMessage;
    // 受信したメッセージを格納
    public List<string> messages;
    // 表示する text
    string displayText;
    void Start()
    {
        MessageSender.OnSendMessage += OnHandleSendedMessage;
        MessageDisplay.OnDisplayMessage += OnHandleDisplayMessage;
        MessageDisplay.OnCompletDisplay += OnHandleCompletDiplay;
    }

    // 送られてきたメッセージを扱う処理
    void OnHandleSendedMessage(string message)
    {
        // 受信したメッセージを追加
        messages.Add(message);
        // 受信したメッセージを表示
        OnReceiveMessage(messages[0]);
    }

    // 受信したメッセージを表示した時の処理
    void  OnHandleDisplayMessage()
    {
        // 表示している message を削除
        messages.RemoveAt(0);
    }

    // 受信したメッセージの表示が終了した時の処理
    void OnHandleCompletDiplay()
    {
        // 表示する message が残っていれば表示する
        if(messages.Count > 0)
        {
            OnReceiveMessage(messages[0]);
        }
    }
}
