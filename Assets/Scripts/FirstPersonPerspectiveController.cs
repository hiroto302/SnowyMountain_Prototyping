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

    // MoveViewPointメソッドで利用する変数群
    // 回転速度
    public float RotateSpeed = 2.0f;
    // X, Y軸 に対する現在の回転角度
    float rotationAngleX, rotationAngleY;
    // 回転初期値
    Quaternion initialRotation;
    // X, Y軸 に対する回転角度の初期値
    Vector3 initialRotationAngle;
    // 上下に向ける角度(回転)を制限
    float maxRotationAngleX = 60.0f;

    // DragMoveViewPointメソッドで使用する変数群
    Vector3 lastMousePosition;
    Vector3 newAngle = Vector3.zero;


    void Reset()
    {
        if(!rb)
        {
            rb = GetComponentInChildren<Rigidbody>();
            viewpointCamera = GetComponentInChildren<Camera>();
        }
    }

    void Start()
    {
        GetInitialRotation();
    }

    void Update()
    {
        MoveViewPoint();
        // DragMoveViewpoint();
    }

    // 視点をマウスの合わせて制御できるようにする
    void MoveViewPoint()
    {
        // 左右方向
        rotationAngleY += Input.GetAxis("Mouse X") * RotateSpeed;

        // 上下方向
        rotationAngleX -= Input.GetAxis("Mouse Y") * RotateSpeed;
        // 上下方向の制限
        if(rotationAngleX > maxRotationAngleX)
        {
            rotationAngleX = maxRotationAngleX;
        }
        else if (rotationAngleX < -maxRotationAngleX)
        {
            rotationAngleX = -maxRotationAngleX;
        }
        // カメラの回転
        transform.rotation = Quaternion.Euler(rotationAngleX, rotationAngleY, 0);
    }

    void GetInitialRotation()
    {
        initialRotation = this.transform.rotation;
        initialRotationAngle = initialRotation.eulerAngles;
        rotationAngleX = initialRotationAngle.x;
        rotationAngleY = initialRotationAngle.y;
    }


    // マウスの左クリック中、ドラッグした時視点を移動するメソッド
    // マウスのクリック位置を取得することでカメラの回転を実装
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
