using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// メッセージを送るクラス
public class MessageSender : MonoBehaviour
{
    // 送るメッセージを格納している Data
    [SerializeField] TextMessageData data = null;
    // 送るメッセージ
    string message = null;
    // メッセージを受信側に送った時の event
    public static event Action<string> OnSendMessage;

    // メッセージを送信するメソッド
    public void SendMessage(int n)
    {
        message = data.TextMessages[n];
        // メッセージを送ったことを知らせる event
        if(OnSendMessage != null)
            OnSendMessage(message);
    }

    // 全てのメッセージを送信
    public void SendAllMessages()
    {
        foreach(string message in data.TextMessages)
        {
            OnSendMessage(message);
        }
    }
}
