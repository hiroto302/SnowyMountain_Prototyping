using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

// Timeline の操作
// UFO が墜落するアニメーションを作成し、このスクリプトから任意のタイミングで再生できるようにする
public class TimelineDirector : MonoBehaviour
{
    PlayableDirector director;
    Coroutine callbackCoroutine;

    // アニメーションのUFOが墜落時に発生する event
    public static event Action OnFallUFO;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    void Start()
    {
        GrabbableFirework.OnIgniteFirework += HandleIgniteFirework;
    }

    // 花火の元が燃やされた時の処理
    void HandleIgniteFirework()
    {
        ExecuteMethodDelay(StartTimeline, 10f);
        // StartTimeline();
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
        // UFO の墜落地点のオブジェクトを入れ替える
        if(OnFallUFO != null)
            OnFallUFO();
    }

    // Timelieを開始
    public void StartTimeline()
    {
        director.Play();
    }

     // メソッドを遅延させて実行するメソッド
    void ExecuteMethodDelay(Action onComplet, float delayTime)
    {
        callbackCoroutine = StartCoroutine(CallBackRoutine(onComplet, delayTime));
    }

    public IEnumerator CallBackRoutine(Action onComplet, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        if(onComplet != null)
        {
            onComplet();
        }
        StopCoroutine(callbackCoroutine);
        callbackCoroutine = null;
    }
}
