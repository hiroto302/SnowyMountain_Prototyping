using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

// ゲームクリア後の制御
// 夕暮れから夜になる
// 星を輝かせる
// 分岐を作成 : 花火を破裂させた時は 仲良しエンド, 松ぼっくりを燃やしただけなら 通常エンド

public class EndingController : MonoBehaviour
{
    // public bool IsEnding = false;
    // エンディングパターン
    public enum EndingPattern {Normal, Good};
    EndingPattern endingPattern = EndingPattern.Normal;
    // Player
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
    // 通常エンドのメッセージ
    [SerializeField] MessageSender normalMessageSender = null;
    // 仲良しエンドのメッセージ
    [SerializeField] MessageSender goodMessageSender = null;
    // クリア後出現させる宇宙人
    [SerializeField] List<GameObject> appearingAliens = null;
    // クリア前に元々いた宇宙人
    [SerializeField] List<GameObject> disappearingAliens = null;
    // クリア後に出現させる特定の object
    [SerializeField] List<GameObject> apperaingObjects = null;

    Coroutine callbackCoroutine = null;

    void Start()
    {
        fadeImage = FindObjectOfType<FadeImage>();
        // ゲームのの目標が達成された時に呼ばれるメソッド
        BonfireController.OnCompletGoal += HandleOnCompletGoal ;
        // 花火が発火された時に呼ばれるメソッド
        GrabbableFirework.OnIgniteFirework += HandleOnIgniteFirework;
    }

    void HandleOnIgniteFirework()
    {
        // 花火が発火されたら good end
        endingPattern = EndingPattern.Good;
    }

    void HandleOnCompletGoal()
    {
        EndGame();
    }

    public void EndGame()
    {
        player.GetComponent<PlayerController>().SetState(PlayerController.State.Waiting);
        // フェード中に Ending の環境に変化 計７秒の処理
        fadeImage.FadeInToOut(3.0f, 1.0f, SetEndingGame);
        // 終了文字を表示
        ExecuteMethodDelay(SendEndingMessage, 7.5f);
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

        // 仲良しエンドの時 エイリアンを配置
        if(endingPattern == EndingPattern.Good)
        {
            foreach(GameObject alien in appearingAliens)
            {
                alien.SetActive(true);
            }
            foreach(GameObject alien in disappearingAliens)
            {
                alien.SetActive(false);
            }
        }

        // 特定のオブジェクトを出現
        if(apperaingObjects != null)
        {
            foreach(GameObject obj in apperaingObjects)
            {
                obj.SetActive(true);
            }
        }
    }

    void SendEndingMessage()
    {
        if(endingPattern == EndingPattern.Normal)
        {
            normalMessageSender.SendAllMessages();
        }
        else if( endingPattern == EndingPattern.Good)
        {
            goodMessageSender.SendAllMessages();
        }
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
