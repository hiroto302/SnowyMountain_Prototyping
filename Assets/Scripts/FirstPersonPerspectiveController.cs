using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 一人称視点でPlayerを制御するクラス

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonPerspectiveController : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb = null;
    // 一人称視点のカメラ
    [SerializeField]
    Camera viewpointCamera = null;

    public float rotationSpeed;

    Vector3 lastMousePosition;
    Vector3 newAngle = Vector3.zero;


    public float rotateSpeed = 2.0f;  //回転の速さ

    void Reset()
    {
        if(!rb)
        {
            rb = GetComponentInChildren<Rigidbody>();
            viewpointCamera = GetComponentInChildren<Camera>();
        }
    }

    void Update()
    {
        DragMoveViewpoint();
    }

    // 視点をマウスの合わせて制御できるようにする

    // マウスの左クリック中、ドラッグした時視点を移動するメソッド
    void DragMoveViewpoint()
    {
        // 左クリックをした時
        if(Input.GetMouseButtonDown(0))
        {
            // 親(この場合は、Eyes)の Transform オブジェクトから見た相対的なオイラー角としての回転値を取得(マウスをドラッグする前のオイラー角)
            newAngle = viewpointCamera.transform.localEulerAngles;
            // Input.mousePosition : GameView に対して、画面の左下を基準に x, y 座標を取得
            lastMousePosition = Input.mousePosition;
        }
        // 左クリックをしている間
        else if(Input.GetMouseButton(0))
        {
            // 左右の回転値
            newAngle.y -= (Input.mousePosition.x - lastMousePosition.x) * 0.1f;
            // 上下の回転値
            newAngle.x -= (Input.mousePosition.y - lastMousePosition.y) * 0.1f;
            // カメラの回転
            viewpointCamera.transform.localEulerAngles = newAngle;
            // マウスの位置更新
            lastMousePosition = Input.mousePosition;
        }
    }

}
