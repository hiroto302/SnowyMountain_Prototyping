using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TexMessageData",menuName = "Data/TextMessage")]
public class TextMessageData : ScriptableObject
{
    // このメッセージを player に送る相手の名称・送信者
    [Header("Sender")]
    [SerializeField] string _Name = "";
    public string Name => _Name;
    // public string Name
    //     {
    //         private set { _Name = value;}
    //         get {return _Name;}
    //     }

    [SerializeField] List<string> _TextMessages = null;
    public IReadOnlyList<string> TextMessages => _TextMessages;
}
