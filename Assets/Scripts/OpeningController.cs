using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// オープニングを制御するクラス
// 何かしらボタンを押したらゲーム開始
// カーソルが中央画面から開始
// 開始画面に目的とか書いていいかも

public class OpeningController : MonoBehaviour
{
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    GameObject openingUIParent;
    [SerializeField]
    SE se = null;
    // 制御するBGM
    BGMController bgm = null;

    bool hasStartedGame = false;

    void Start()
    {
        bgm = GameObject.Find("BGM").GetComponent<BGMController>();
        ConfigureOpening();
    }
    void Update()
    {
        if(Input.anyKeyDown && hasStartedGame == false)
        {
            StartGame();
        }
    }

    // オープニング制御
    void ConfigureOpening()
    {
        // playerを待機状態
        // この処理だけではカーソルが動くならスリプトを無効状態にした方がいいかも
        playerController.SetState(PlayerController.State.Waiting);
    }
    // ゲームを開始するメソッド
    public void StartGame()
    {
        hasStartedGame = true;
        // playerの待機状態解除
        playerController.SetState(PlayerController.State.Normal);
        // 表示文字を消す
        openingUIParent.SetActive(false);
        // 開始音SE
        se.PlaySE(0);
        // BGM再生開始
        bgm.PlayBGM();
    }
}
