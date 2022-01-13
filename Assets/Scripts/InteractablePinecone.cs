using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 掴まれて、炎の中に入れられた時行いこと
// その松ぼっくりは掴まれいる状態なのか？
// 掴まれている状態で、焚き火にインタラクトした時手元から消える(または円軌道を描いて炎の中へ)
public class InteractablePinecone : MonoBehaviour
{
    // 通常・掴まれている状態
    public enum State
    {
        Normal,
        Grabbed
    }
    public State currentState;

    public string objectName = "Pinecone";

    void Awake()
    {
        SetState(State.Normal);
    }

    void Start()
    {
        BonfireController.IntoFire += GoIntoFire;
        Grabbable.OnChangeState += ChangeState;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && (currentState == State.Grabbed))
        {

        }
    }

    void GoIntoFire()
    {
        // 掴まれいる松ぼっくりに対して
        if(currentState == State.Grabbed)
        {
            // 消す
            InteractHandler.GrabbedObjectName = null;
            Destroy(this.gameObject);
        }
    }

    public void SetState(State state)
    {
        currentState = state;
    }

    public void ChangeState(string name)
    {
        if(name == objectName && currentState == State.Grabbed)
        {
            SetState(State.Normal);
        }
        // else if(name == objectName && currentState == State.Normal)
        // {
        //     SetState(State.Grabbed);
        // }
    }
    public void ChangeCurrentState()
    {
        if(InteractHandler.GrabbedObjectName == objectName && currentState == State.Grabbed)
        {
            SetState(State.Normal);
        }
        else if(InteractHandler.GrabbedObjectName == objectName && currentState == State.Normal)
        {
            SetState(State.Grabbed);
        }
    }
}
