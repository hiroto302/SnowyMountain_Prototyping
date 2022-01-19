using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player の物を掴む手にアタッチ
public class Grabber : MonoBehaviour
{
    // 右手 or 左手
    public enum Hand { R, L };
    [SerializeField] Hand hand = Hand.R;

    // 物を掴んでいる状態か
    public enum State { Normal, Grabbing };
    public State state = State.Normal;

    // 現在掴んでいる物の名前
    public static string GrabbingObjectName = null;
    // 掴んでいるオブジェクトのクラスコンポーネント
    public GrabbableObject grabbable = null;

    void Start()
    {
        GrabbableObject.OnGrabbed += GrabObject;
    }

    // インタラクトして、GrabbableObject のスクリプトを持つなら手元へ移動した時に呼ばれる
    // 既に物を掴んでいたら、掴んでいる物を離して新しいものを掴む
    public void GrabObject()
    {
        // 既に物を掴んでいたら
        if(state == State.Grabbing)
            ReleaseGrabbedObject();

        grabbable = GetComponentInChildren<GrabbableObject>();
        SetState(State.Grabbing);
        GetObjectName(grabbable.ObjectName);
    }
    // 掴んでいるものを離す
    public void ReleaseGrabbedObject()
    {
        grabbable.DetachFromHand();
        SetState(State.Normal);
        GrabbingObjectName = null;
    }
    // 状態を変更するメソッド
    public void SetState(State state)
    {
        this.state = state;
    }
    // 掴んでいるオブジェクト名を取得
    public void GetObjectName(string name)
    {
        GrabbingObjectName = name;
    }
}
