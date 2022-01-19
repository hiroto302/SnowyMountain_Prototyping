using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Player を制御するクラス
public class PlayerController : MonoBehaviour
{
    // Player の状態 通常・待機状態
    public enum State { Normal, Waiting};
    public State state;
    // 移動を制御しているクラス
    [SerializeField] FirstPersonPerspectiveController moveController;
    // 物を掴む制御しているクラス
    [SerializeField] Grabber grabber = null;
    // Player の State が変化した時に発生する event
    public event Action<State> onStateChange;

    void Awake()
    {
        SetState(State.Normal);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            grabber.ReleaseGrabbedObject();
        }
    }

    // Player の状態を変更するメソッド
    public void SetState(State state)
    {
        this.state = state;
        if(state == State.Normal)
        {
            if(onStateChange != null)
                onStateChange(state);

            moveController.ToggleMovableState(true);
        }
        else if(state == State.Waiting)
        {
            if(onStateChange != null)
                onStateChange(state);

            moveController.ToggleMovableState(false);
        }
    }
}
