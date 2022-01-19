using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// 掴むことが可能なオブジェクトにアタッチ
// 掴んだ時にPlayer と衝突判定が起きないよにレイヤーで設定すること
// 今回は InteractHandle.cs により OnInteract.cs の UnityEvent が呼ばれた時、このスクリプトの機能を 登録して実行されるようにする

public class Grabbable : MonoBehaviour
{
    // player の手元の位置
    [SerializeField] Transform handTransform;
    [SerializeField] Transform snapOffset;

    Rigidbody rb;

    // 掴むもの名前
    public string ObjectName = null;

    // オブジェクトが掴む or 掴まれいる状態かの時に起こる event : 引数 掴んでいるもの名称
    public static event Action<string> OnChangeState;

    void Reset()
    {
        handTransform = GameObject.FindGameObjectWithTag("Hand").GetComponent<Transform>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // 掴まれたら Player の手元(Hand)の子オブジェクトに移動する。移動した時のHandに対しての相対位置をあらかじめ決定しておく。
    // 手元へ移動するメソッド
    public void MoveToHand()
    {
        // 掴んでるの時の物理挙動
        rb.isKinematic = true;
        // 親オブジェクトにPlayerのHandを指定
        // SetParent の挙動が指定した親の親にも依存してscale が変化してしまう。 なので、Scale(1, 1, 1) の空オブジェクト(今回は、Hand_root)作成し、object 構造に注意すること
        // or //ワールド座標系→ローカル座標系の係数を作成してあげるとか
        transform.SetParent(handTransform, true);
        // 移動した時のローカル位置を指定
        if(snapOffset!)
        {
            transform.localPosition = snapOffset.position;
            transform.localRotation = snapOffset.rotation;
        }

        InformGrabbedObjectName();
        OnChangeState(ObjectName);
    }

    // 手元から離れるメソッド
    public void DetachFromHand()
    {
        transform.SetParent(null, true);
        rb.isKinematic = false;
        // transform.localScale = lossyScale;

        // 掴んでいる物なし
        InteractHandler.GrabbedObjectName = null;
        OnChangeState(ObjectName);
    }

    // 掴んでいるものが何であるか?
    public void GetGrabbedObjectName(string name)
    {
        // 掴んでいる物に固有名詞がある時実行
        this.ObjectName = name;
    }

    // 物を掴んだことを知らせる
    public void InformGrabbedObjectName()
    {
        // 掴んだ物に固有名詞がある時
        if(ObjectName != null)
        {
            InteractHandler.GrabbedObjectName = ObjectName;
        }
    }
}
