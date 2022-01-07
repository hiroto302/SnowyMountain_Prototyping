using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 矢印キーによるカメラ制御
public class CameraController : MonoBehaviour
{
    //回転速度
    public float rotationSpeed = 1f;
    //x軸回転角度の最大値
    public float max_rotation_x = 60f;
    //現在の回転角度
    private float rotation_x = 0f;
    private float rotation_y = 0f;

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow)){
            //回転角度を変更
            rotation_y -= rotationSpeed;
            //y軸を軸に左回りにrotationSpeed度回転
            transform.rotation = Quaternion.Euler(rotation_x, rotation_y, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //回転角度を変更
            rotation_y += rotationSpeed;
            //y軸を軸に左回りにrotationSpeed度回転
            transform.rotation = Quaternion.Euler(rotation_x, rotation_y, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            //カメラの縦方向の角度の範囲を指定
            if(rotation_x < -max_rotation_x){
                //範囲外のときreturn
                return;
            }
            //回転角度を変更
            rotation_x -= rotationSpeed;
            //x軸を軸に上方向に回転
            transform.rotation = Quaternion.Euler(rotation_x, rotation_y, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            //カメラの縦方向の角度の範囲を指定
            if (rotation_x > max_rotation_x)
            {
                //範囲外のときreturn
                return;
            }
            //回転角度を変更
            rotation_x += rotationSpeed;
            //x軸を軸に上方向に回転
            transform.rotation = Quaternion.Euler(rotation_x, rotation_y, 0);
        }
    }
}
