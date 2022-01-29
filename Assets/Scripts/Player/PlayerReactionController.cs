using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 特定の出来事が発生した時に起きる、Player のリアクション制御

[RequireComponent(typeof(MessageSender))]
public class PlayerReactionController : MonoBehaviour
{
    [SerializeField] MessageSender messageSender = null;
    [SerializeField] Collider encounterTrigger = null;
    [SerializeField] Collider afterEncounterTrigger = null;

    bool hasFallenAlien = false; // エイリアンが落ちたか
    bool hasMetAlien = false;    // エイリアンに出会ったか

    void Start()
    {
        TimelineDirector.OnFallUFO += HandleFallUFO;
    }

    // UFO が墜落した時に行う処理
    void HandleFallUFO()
    {
        hasFallenAlien = true;
        encounterTrigger.enabled = true;
    }

    // 宇宙人に出会った時の反応
    public void ReactToAlienEncounter()
    {
        if(hasFallenAlien)
        {
            Debug.Log("react1");
            messageSender.SendMessage(2);
            hasMetAlien = true;
            afterEncounterTrigger.enabled = true;
        }
    }
    // 出会った後の反応
    public void ReactToAlienEncounterAfterEvent()
    {
        Debug.Log("react2");
        messageSender.SendMessage(3);
    }
}
