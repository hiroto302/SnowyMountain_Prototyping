using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbablePinecone : GrabbableObject
{
    public override void Start()
    {
        base.Start();
        BonfireController.IntoFire += GoIntoFire;
    }

    // 松ぼっくりが火の中に放たれる時の処理
    void GoIntoFire()
    {
        if(currentState == State.Grabbed)
        {
            Grabber grabber = GetComponentInParent<Grabber>();
            grabber.ReleaseGrabbedObject();
            Destroy(this.gameObject);
        }
    }
}
