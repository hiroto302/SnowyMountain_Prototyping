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


    void Start()
    {
        
    }

    void Update()
    {
        movementPosition = GetMovementPosition();
    }

    void FixedUpdate()
    {
        MovePlayerPosintion(movementPosition);
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
}
