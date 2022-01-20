using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 送られてくるメッセージを受け取るクラス
public class MessageReceiver : MonoBehaviour
{
    // メッセージを受信した時の event
    public static event Action<string> OnReceiveMessage;
    void Start()
    {
        MessageSender.OnSendMessage += OnHandleSendedMessage;
    }

    // 送られてきたメッセージを扱う処理
    void OnHandleSendedMessage(string message)
    {
        OnReceiveMessage(message);
    }
}
