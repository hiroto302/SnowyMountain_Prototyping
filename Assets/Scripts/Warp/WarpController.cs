using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ワープを実行するための機能
// 開始位置と目的先を指定する
namespace WarpObject
{
    public static class WarpController
    {
        // 対象の位置を目的地に移動させるメソッド
        public static void Warp(GameObject target, Transform destination)
        {
            // 位置移動
            target.transform.position = destination.position;
            // 移動時の回転方向
        //     float destinationRotationY = destination.transform.rotation.eulerAngles.y;
        //     target.transform.rotation = Quaternion.Euler(0, destinationRotationY, 0);
            target.transform.rotation = destination.rotation;
        }

        public static void Warp(GameObject target, Transform destination, float angleY)
        {
            target.transform.position = destination.position;
            // 移動対象を回転させる
            float destinationRotationY = destination.transform.rotation.eulerAngles.y;
            target.transform.rotation = Quaternion.Euler(0, destinationRotationY, 0);
        }
    }
}
