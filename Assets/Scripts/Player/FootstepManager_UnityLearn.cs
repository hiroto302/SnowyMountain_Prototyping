using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// UnityLearn 学んだことを参考に 対応する足音をシンプルに Object の tag によって判別し、
// 足音を鳴らすことを実行するスクリプトを保存しておく。
public class FootstepManager_UnityLearn : MonoBehaviour
{
    AudioSource source;
    // ここの例では、雪・砂・水 に対応する足音を実行することを想定とする
    public List<AudioClip> snowSteps = new List<AudioClip>();
    public List<AudioClip> sandSteps = new List<AudioClip>();
    public List<AudioClip> waterSteps = new List<AudioClip>();
    // 地表の変数
    enum Surface {snow, sand , water};
    Surface surface;
    // 現在playerが立っている地表に対応した複数の足音 AudioClip の list を格納
    List<AudioClip> currentStepsList;

    // 足音を鳴らすメソッド
    public void PlayStep()
    {
        // ランダムに音を格納
        AudioClip clip = currentStepsList[Random.Range(0, currentStepsList.Count)];
        // 再生
        source.PlayOneShot(clip);
    }

    // 地表に対応した音を currentStepsListに格納
    void SelectStepList()
    {
        switch (surface)
        {
            case Surface.snow:
                currentStepsList = snowSteps;
                break;
            case Surface.sand:
                currentStepsList = sandSteps;
                break;
            case Surface.water:
                currentStepsList = waterSteps;
                break;
        }
    }

    // プレイヤーが歩いているサーフェイスの種類を検出する処理
    // 今回作成したPlayerには、アニメーションで制御するような足は無いので常に足がついている状態。なので OnTriggerEnter(足が交互に着くたびに呼ばれる) ではなく
    // OnTriggerStay で呼び出し続けける。
    void OnTriggerStay(Collider other)
    {
        // 歩いている地表を判断したら SelectStepListを実行する
        if(other.gameObject.tag == "Snow")
        {
            surface = Surface.snow;
        }
        else if( other.gameObject.tag == "Sand" )
        {
            surface = Surface.sand;
        }
        else if( other.gameObject.tag == "Water" )
        {
            surface = Surface.water;
        }
        SelectStepList();
    }

}
