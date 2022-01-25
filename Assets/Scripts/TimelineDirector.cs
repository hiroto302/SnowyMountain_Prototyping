using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// Timeline の操作
// UFO が墜落するアニメーションを作成し、このスクリプトから任意のタイミングで再生できるようにする
public class TimelineDirector : MonoBehaviour
{
    PlayableDirector director;
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
            StartTimeline();
    }

    // Timeline開始時に実行したい処理
    void Director_Played(PlayableDirector obj)
    {
        
    }
    // Timline停止時に実行したい処理
    void Director_Stopped(PlayableDirector obj)
    {

    }

    // Timelieを開始
    public void StartTimeline()
    {
        director.Play();
    }
}
