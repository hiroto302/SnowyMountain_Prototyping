using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゲームの Quality を管理するクラス
// スクリプトからランタイムに quality を制御することが可能なのか？
public class QualityMangager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(QualitySettings.renderPipeline + " : QualitySettings.renderPipeline");
        Debug.Log(QualitySettings.GetQualityLevel() + " : QualityLevel");
        /*
        Quality に追加されている Level の上から順に 0(Low), 1(Medium), 2(High) が デフォルトで設定してある。
        各Level に設定してある RenderPipeline が使用される
        */
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            QualitySettings.SetQualityLevel(2);
            Debug.Log(QualitySettings.renderPipeline + " : QualitySettings.renderPipeline");
            Debug.Log(QualitySettings.GetQualityLevel() + " : QualityLevel");
        }
    }
}
