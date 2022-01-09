using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Player のユーザーインターフェースであるカーソルを制御するクラス
// 実装したいこと
// カーソルを中央に固定(プレイ中), 設定オプションなどを開いている時は自由に動かせるようにする
// 中央に固定したカーソルのアイコンを変更
// 他のオブジェクトと干渉できる場合はアイコンが変化できたりするようにする

public class InteractHandler : MonoBehaviour
{
    public GameObject CentralPointerPrefab;   // Player の中心点
    public Sprite InteractablePointer;  // Player が相互作用可能なもの時の pointer
    public Sprite NormalPointer;        // 通常時の pointer
    Image pointerImage;                 // Image Componet : pointer の sprite を状況によって変更する箇所
    Vector3 originalPointerSize;        //  元のサイズ

    float interactRange = 2.0f;

    void Start()
    {
        // CenterPointer の生成
        Instantiate(CentralPointerPrefab);

        var centerPoint= GameObject.Find("CenterPoint");
        if(centerPoint != null)
        {
            pointerImage = centerPoint.GetComponent<Image>();
            originalPointerSize = centerPoint.transform.localScale;
        }
    }

    void Update()
    {
        OnInteract[] targets = null;
        // ビューポート座標を介して、カメラからレイを飛ばす。ビューポート座標は正規化されカメラと関係し,カメラの左下は (0, 0) で、右上が (1, 1)
        // 引数(0.5, 0.5, 0.5) つまり画面の中央位置(アイコンのある位置)からray を飛ばす (ｚ座標は無視されてるよ)
        var ray = Camera.main.ViewportPointToRay(Vector3.one * 0.5f);
        RaycastHit hit;

        bool displayInteractable = false;

        // interact 可能な Object が一定の範囲にある時
        if(Physics.Raycast(ray, out hit, interactRange))
        {
            // hitしたオブジェクトの子オブジェクトにある全てのOnInteractスクリプトコンポーネントを取得
            var interacts = hit.collider.gameObject.GetComponentsInChildren<OnInteract>();
            if (interacts.Length > 0)
            {
                displayInteractable = true;
                targets = interacts;
                pointerImage.color = Color.white;

                foreach (var target in targets)
                {
                    // interact 可能な状態で無い時
                    if (!target.isActiveAndEnabled)
                    {
                        pointerImage.color = Color.grey;
                        break;
                    }
                }
            }
        }

        // Interact した時に行うメソッドを実行 Input.GetKeyDown(KeyCode.E)
        if (targets != null &&
            (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)))
        {
            foreach(var target in targets)
            {
                if(target.isActiveAndEnabled)
                    target.Interact();
            }
        }

        // InteractableObject に対するポインターの制御
        if(displayInteractable)
        {
            // sprite を変更し、サイズを大きくする
            pointerImage.sprite = InteractablePointer;
            pointerImage.transform.localScale = originalPointerSize * 1.5f;
        }
        else
        {
            pointerImage.sprite = NormalPointer;
            pointerImage.color = Color.white;
            pointerImage.transform.localScale = originalPointerSize;
        }
    }
}
