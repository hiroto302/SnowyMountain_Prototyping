using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// 必要数の松ぼっくりを入れたら発火する機能を追加
public class BonfireController : MonoBehaviour
{
    // 炎 VFX : 燃えている時に発生する
    public ParticleSystem FirePartice;
    // 点火する時に発生する VFX : 煙みたいなやつ
    public ParticleSystem IgniteParticle;
    // 炎の明かり
    public GameObject PointLight;
    // 焚火に必要な松ぼっくりの数
    public int RequiredPineconesNum = 3;
    // 焚き火の中にある松ぼっくりの数
    public int InFirePineconesNum = 0;

    // 松ぼっくりが炎の中へ入れらた時発生する event
    public static event Action IntoFire;

    [SerializeField]
    SE se = null;
    [SerializeField]
    AudioSource fireParticeAudioSource = null;

    [SerializeField]
    MessageSender messageSender = null;

    Coroutine callbackCoroutine;
    // ゲームの目標達成時に発生する event
    public static event Action OnCompletGoal;


    void Start()
    {
        fireParticeAudioSource.Play();
        fireParticeAudioSource.volume = 0.05f;
    }
    // 発火メソッド
    public void Ignite()
    {
        // 必要数の松ぼっくりがあれば発火
        if(RequiredPineconesNum <= InFirePineconesNum)
        {
            FirePartice.Play();
            PointLight.SetActive(true);
            // fireParticeAudioSource.Play();
            fireParticeAudioSource.volume = 0.3f;
            if(IgniteParticle!)
                IgniteParticle.Play();

            // 発火したら GameEnging
            messageSender.SendMessage(1);
            // 目標の達成
            ExecuteMethodDelay(OnCompletGoal, 5.0f);
        }
        // 発火条件を満たさない時
        else
        {
            if(IgniteParticle!)
            {
                IgniteParticle.Play();
                se.PlaySE(1);
            }
        }
    }

    // 火の中の松ぼっくりを入れた時に発生するメソッド
    public void PutPineconeIntoFire()
    {
        // if(InteractHandler.GrabbedObjectName == "Pinecone")
        // {
        //     InFirePineconesNum ++;
        //     IntoFire();
        //     Ignite();
        // }

        if( Grabber.GrabbingObjectName == "Pinecone" )
        {
            InFirePineconesNum ++;
            Ignite();
            IntoFire.Invoke();
        }
        else
        {
            // 松ぼっくりが無いことを伝える
            messageSender.SendMessage(0);
            se.PlaySE(2);
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
