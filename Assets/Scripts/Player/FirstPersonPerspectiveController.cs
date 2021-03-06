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
    public float RotateSpeed = 0.5f;
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

    // Player の移動速度
    public float MovementSpeed = 3.0f;
    Vector3 moveDirection;

    float inputValueX;
    float inputValueZ;

    // 足音（移動時の音)
    [SerializeField]
    FootstepManager footstepManager = null;
    float elapsedTime = 0;
    float footstepInterval = 0.8f; // 足音が鳴る間隔

    // 移動を行うことが可能であるか
    public bool isMovable;

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
        GetInputValue();
    }

    void FixedUpdate()
    {
        MoveViewPoint();
        if(isMovable)
            MovePlayer();
    }

    // 移動可能状態の切り替え
    public void ToggleMovableState(bool  movableState)
    {
        this.isMovable = movableState;
        // 動ける可能な状態になった時
        // ワープ機能を実装して、ワープ先の player 位置・回転を指定した時、
        // 回転のための初期値を更新する必要があるため GetInitialRotaion を呼び出す。
        if(movableState == true)
        {
            GetInitialRotation();
        }
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

    // 視点感度の変更 : 初期値 0.5。調整できるのは0.2 ~ 0.8 の間くらい
    public void SetRotateSpeed(float speed)
    {
        RotateSpeed = speed;
    }

    public void GetInitialRotation()
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

    // 入力値の取得
    void GetInputValue()
    {
        // 入力値を取得(-1~1)
        inputValueX = Input.GetAxis("Horizontal");
        inputValueZ = Input.GetAxis("Vertical");
    }
    // Player の位置移動を実装する
    void MovePlayer()
    {
        // 前後移動 (どこの方向を向いていても正面に進めるようにする)

        // 下記の二行は、y軸の回転量を Quaternion 型で取得できるのなら省略可能、またはそれと同等の操作をする(オイラー角をまったく使用しないで回転を作成し操作したい)
        // ｙ軸の回転量
        float myAngleY = transform.eulerAngles.y;

        // Quaternion rotationValue = Quaternion.Euler(new Vector3(0, myAngleY, 0)); // y軸周りの回転
        // or
        Quaternion rotationValueY = Quaternion.AngleAxis(myAngleY, Vector3.up);   // axis の周りを angle 度回転する回転を作成

        // 注意:Quaternion * Vector3 の順で使用する必要があり、これは与えられたベクトルを回転させることを意味する
        // or on two Quaternion for using the first rotation an on top of it add the second one (order matters!)

        // Vector3 newDirection = rotationValue * Vector3.forward;
        // or
        Vector3 frontDirection = rotationValueY * Vector3.forward;

        // rb.velocity = newDirection.normalized * inputValueZ * MovementSpeed;
        // rb.velocity = frontDirection.normalized * inputValueZ * MovementSpeed;


        // 左右方向取得
        Vector3 rightDirection = transform.right;

        // 走り or 歩き
        if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            MovementSpeed = 5.0f;
            footstepInterval = 0.6f;
        }
        else
        {
            MovementSpeed = 3.0f;
            footstepInterval = 0.8f;
        }

        // 前後左右の移動
        // 上下の移動は、重力がPlayerにかかるようデフォルトのまま。上書きしないように
        Vector3 forceDirection = (rightDirection * inputValueX + frontDirection.normalized * inputValueZ)  * MovementSpeed;
        rb.velocity = new Vector3(forceDirection.x, rb.velocity.y, forceDirection.z);

        // 足音
        if(Mathf.Abs(inputValueX) > footstepInterval || Mathf.Abs(inputValueZ) > footstepInterval)
        {
            elapsedTime += Time.fixedDeltaTime;
            if(elapsedTime > footstepInterval)
            {
                footstepManager.PlayFootstepSE();
                elapsedTime = 0;
            }
        }
    }

}
