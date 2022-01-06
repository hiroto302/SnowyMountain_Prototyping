using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Playerを制御するクラス
public class PlayerController : MonoBehaviour
{

    [SerializeField]
    Rigidbody rb = null;

    // Player の移動速度
    public float MovementSpeed = 3.0f;

    // x・z 方向の入力値
    float inputValueX, inputValueZ;
    // 現在位置からの相対的な移動位置
    Vector3 movementPosition;

    // 現在の位置
    Vector3 currentPosition;


    void Start()
    {
        currentPosition = transform.position;
    }

    void Update()
    {
        movementPosition = GetMovementPosition();
    }

    void FixedUpdate()
    {
        MovePlayerPosintion(movementPosition);
        RotateMovementDirection();
    }

    // player の移動位置を取得
    Vector3 GetMovementPosition()
    {
        // player の移動入力
        inputValueX = Input.GetAxis("Horizontal");
        inputValueZ = Input.GetAxis("Vertical");
        Vector3 movementPosition = new Vector3(inputValueX * MovementSpeed, 0, inputValueZ * MovementSpeed);
        return movementPosition;
    }
    // player の位置移動
    void MovePlayerPosintion(Vector3 movementPosition)
    {
        rb.velocity = this.movementPosition;
    }

    // 3人称視点操作に合わせた player の回転移動
    // 移動に合わせて player の正面方向を回転させるメソッド
    void RotateMovementDirection()
    {
        // 移動方向ベクトルの取得( 回転させる方向)
        Vector3 directionVector = new Vector3(transform.position.x, 0, transform.position.z) - new Vector3(currentPosition.x, 0, currentPosition.z);
        // 移動後の現在位置を更新
        currentPosition = transform.position;
        // 一定距離移動した場合回転
        if (Mathf.Abs(directionVector.x) > 0.001f || Mathf.Abs(directionVector.z) > 0.001f)
        {
            // 回転量を取得
            Quaternion rot = Quaternion.LookRotation(directionVector);
            // 緩やかに回転
            this.transform.rotation = Quaternion.Slerp(rb.transform.rotation, rot, 0.1f);
        }
    }
}
