using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 掴むことが可能なオブジェクトにアタッチするクラス
// 各オブジェクトが継承して利用することを考慮して作成する
// 掴んだ時にPlayer と衝突判定が起きないよにレイヤーで設定すること
// 今回は InteractHandle.cs により OnInteract.cs の UnityEvent が呼ばれた時、このスクリプトの機能を 登録して実行されるようにする
// Grabbable ではなくこちらを活用していく

public class GrabbableObject : MonoBehaviour
{
    // player の手元の位置
    [SerializeField] Transform handTransform;
    [SerializeField] Transform snapOffset;

    Rigidbody rb;

    // 掴むもの名前
    public string ObjectName = null;

    // 掴まれている状態か
    public enum State
    {
        Normal,
        Grabbed
    }
    public State currentState;

    // オブジェクトが掴む or 掴まれいる状態かの時に起こる event : 引数 掴んでいるもの名称
    // public static event Action<string> OnChangeState;
    public static event Action OnGrabbed;

    void Reset()
    {
        // 移動する手を取得
        handTransform = GameObject.FindGameObjectWithTag("Hand").GetComponent<Transform>();
    }

    public virtual void Start()
    {
        if(handTransform == null)
            handTransform = GameObject.FindGameObjectWithTag("Hand").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        SetState(State.Normal);
    }

    // 手元へ移動するメソッド
    public void MoveToHand()
    {
        // 掴んでるの時の物理挙動
        rb.isKinematic = true;
        // 親オブジェクトにPlayerのHandを指定
        transform.SetParent(handTransform, true);
        // 移動した時のローカル位置を指定
        if(snapOffset!)
        {
            transform.localPosition = snapOffset.position;
            transform.localRotation = snapOffset.rotation;
        }

        SetState(State.Grabbed);

        // 物を掴んだことを知らせる event
        OnGrabbed.Invoke();
    }

    // 手元から離れるメソッド
    public void DetachFromHand()
    {
        transform.SetParent(null, true);
        rb.isKinematic = false;

        SetState(State.Normal);
    }

    // 状態を変更するメソッド
    public virtual void SetState(State state)
    {
        currentState = state;
    }
}
