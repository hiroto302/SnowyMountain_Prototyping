using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player のユーザーインターフェースであるカーソルを制御するクラス
// 実装したいこと
// カーソルを中央に固定(プレイ中), 設定オプションなどを開いている時は自由に動かせるようにする
// 中央に固定したカーソルのアイコンを変更
// 他のオブジェクトと干渉できる場合はアイコンが変化できたりするようにする
public class CursorManager : MonoSingletone<CursorManager>
{
    void Start()
    {
        LockCursor();
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void UnLockCurcor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
