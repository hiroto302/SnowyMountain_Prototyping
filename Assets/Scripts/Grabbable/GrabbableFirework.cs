using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// このスクリプトをアタッチしたオブジェクトを火の中に入れると花火が打ち上がる
public class GrabbableFirework : GrabbableObject
{
    // 焚火に入れらて燃やされた時に発生する event
    public static event Action OnIgniteFirework;
    public override void Start()
    {
        base.Start();
        BonfireController.IntoFire += GoIntoFire;
    }

    // 火の中に入った時の処理
    void GoIntoFire()
    {
        if(currentState == State.Grabbed)
        {
            OnIgniteFirework();
            Grabber grabber = GetComponentInParent<Grabber>();
            grabber.ReleaseGrabbedObject();
            Destroy(this.gameObject);
        }
    }
}
