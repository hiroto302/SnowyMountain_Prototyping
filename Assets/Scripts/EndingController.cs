using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

// ゲームクリア後の制御
// 夕暮れから夜になる
// 星を輝かせる
// 花火が炸裂した時は、友人が出現。食器とか食べ物を置いて和気藹々な雰囲気な中終了
// やはり文字を表示できたらいいよね

public class EndingController : MonoBehaviour
{
    public bool IsEnding = false;
    [SerializeField] GameObject player = null;
    // ending が始まる時のplayer の位置
    [SerializeField] Transform endingTransform = null;
    // 星の煌めき
    [SerializeField] GameObject stars = null;
    // directionalLight
    [SerializeField] Light directionalLight;
    // 色
    Color nightColor;
    FadeImage fadeImage;

    [SerializeField] Material nightSky = null;
    [SerializeField] MessageSender messageSender = null;
    Coroutine callbackCoroutine = null;


    void Start()
    {
        fadeImage = FindObjectOfType<FadeImage>();
        // ゲームのの目標が達成された時に呼ばれるメソッド
        BonfireController.OnCompletGoal += OnHandleCompletGoal ;
    }

    void OnHandleCompletGoal()
    {
        EndGame();
    }

    public void EndGame()
    {
        player.GetComponent<PlayerController>().SetState(PlayerController.State.Waiting);
        // フェード中に Ending の環境に変化 計７秒の処理
        fadeImage.FadeInToOut(3.0f, 1.0f, SetEndingGame);
        // 終了文字を表示
        ExecuteMethodDelay(SendMessage, 7.5f);
        // 隕石とか流れ星とか降らせたい
        // ステージの美化
    }

    void SetEndingGame()
    {
        // 夜空になる
        RenderSettings.skybox = nightSky;
        // 星を出す
        stars.SetActive(true);
        Color newColor;
        // DirectionalLight の色を変更
        if ( ColorUtility.TryParseHtmlString("000129", out newColor))
            { nightColor = newColor; }
        directionalLight.color = nightColor;
        // 環境光の色を変更
        RenderSettings.ambientLight = new Color(90f/255f, 63f/255f, 178f/255f, 1f);
        // RenderSettings.ambientIntensity = -1f;

        // Ending 時のPlayer の位置 と 状態(視点だけ変えられる状態) を変更
        player.transform.position = endingTransform.position;
        player.transform.rotation = endingTransform.rotation;
        player.GetComponent<PlayerController>().SetState(PlayerController.State.Normal);
    }

    void SendMessage()
    {
        messageSender.SendAllMessages();
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
