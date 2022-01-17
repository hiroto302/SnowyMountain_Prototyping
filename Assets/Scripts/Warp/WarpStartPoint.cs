using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WarpObject;

// ワープスタート地点
public class WarpStartPoint : MonoBehaviour
{
    // ワープ開始位置
    Transform startPoint;
    // ワープをさせる対象
    public GameObject target;
    // ワープ先の目的地
    public Transform destination;

    // ワープの種類
    public enum Type { Normal, Walk};
    public Type type;

    // fade画像
    FadeImage fadeImage;
    // 足音
    FootstepManager footstepManager;
    // Player
    PlayerController playerController;
    Coroutine walkCoroutine;


    void Start()
    {
        fadeImage = FindObjectOfType<FadeImage>();
        footstepManager = FindObjectOfType<FootstepManager>();
        playerController = FindObjectOfType<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        // ターゲット が player の時
        if(other.gameObject == target && other.gameObject.tag == "Player")
        {
            // 瞬間移動
            if(type == Type.Normal)
            {
                WarpController.Warp(target, destination);
                playerController.SetState(PlayerController.State.Normal);
            }

            // 歩行してたどり着いたような演出
            if(type == Type.Walk)
            {
                walkCoroutine = StartCoroutine(WalkWarpRoutine());
            }
        }
    }

    IEnumerator WalkWarpRoutine()
    {
        int footStepCount = 0;
        playerController.SetState(PlayerController.State.Waiting);
        fadeImage.FadeIn(3.0f);
        while(footStepCount < 3)
        {
            yield return new WaitForSeconds(0.8f);
            footstepManager.PlayFootstepSE();
            footStepCount++;
        }
        yield return new WaitForSeconds(3.0f);
        WarpController.Warp(target, destination);
        playerController.SetState(PlayerController.State.Normal);
        fadeImage.FadeOut(3.0f);
        StopCoroutine(walkCoroutine);
        walkCoroutine = null;
    }
}
