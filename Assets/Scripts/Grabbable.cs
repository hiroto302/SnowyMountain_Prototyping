using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 掴むことが可能なオブジェクトにアタッチ
// 掴んだ時にPlayer と衝突判定が起きないよにレイヤーで設定すること
// 今回は InteractHandle.cs により OnInteract.cs の UnityEvent が呼ばれた時、このスクリプトの機能を 登録して実行されるようにする

public class Grabbable : MonoBehaviour
{
    // player の手元の位置
    public Transform HandTransform;
    public Transform SnapOffset;

    Rigidbody rb;
    Vector3 lossyScale;
    Vector3 localeScale;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        // ワールド座標に対する、従来のサイズを保存
        lossyScale = transform.lossyScale;
    }
    void Update()
    {
        // テスト用
        if(Input.GetKeyDown(KeyCode.R))
            DetachFromHand();

        if(Input.GetKeyDown(KeyCode.T))
            MoveToHand();
    }

    // 掴まれたら Player の手元(Hand)の子オブジェクトに移動する。移動した時のHandに対しての相対位置をあらかじめ決定しておく。
    // 手元へ移動するメソッド
    public void MoveToHand()
    {
        // 掴んでるの時の物理挙動
        rb.isKinematic = true;
        // 親オブジェクトにPlayerのHandを指定
        // SetParent の挙動が指定した親の親にも依存してscale が変化してしまう。 なので、Scale(1, 1, 1) の空オブジェクト(今回は、Hand_root)作成し、object 構造に注意すること
        transform.SetParent(HandTransform, true);
        // transform.localScale = originalSize;
        // 移動した時のローカル位置を指定
        if(SnapOffset!)
        {
            transform.localPosition = SnapOffset.position;
            transform.localRotation = SnapOffset.rotation;
        }
    }

    // 手元から離れるメソッド
    public void DetachFromHand()
    {
        transform.SetParent(null, true);
        rb.isKinematic = false;
        // transform.localScale = lossyScale;
    }
}
