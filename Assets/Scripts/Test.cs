using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


// テスト用クラス
public class Test : MonoBehaviour
{

    // 使用されている GraphicsAPI
    [SerializeField] TextMeshProUGUI textGAPI = null;
    string graphicsAPI;

    // 経過時間を測定
    Coroutine timerCoroutine;
    [SerializeField] TextMeshProUGUI elapsedTime = null;
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            ExecuteTest();
        }
    }

    void ExecuteTest()
    {
        Debug.Log(SystemInfo.graphicsDeviceType + " graphics API type ");
        graphicsAPI = SystemInfo.graphicsDeviceType.ToString();
        textGAPI.text = graphicsAPI;

        TestCoroutine();
    }

    void TestCoroutine()
    {
        timerCoroutine =  StartCoroutine(CountTimeRoutine());
        // Debug.Log("Start Coroutine");
    }
    IEnumerator CountTimeRoutine()
    {
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        float elapsed = 0;
        float max = 10.0f;
        float waitTime = 1.0f;
        while(max > elapsed)
        {
            elapsed += waitTime;
            yield return new WaitForSeconds(waitTime);
            // Debug.Log(elapsed + " : elapsedTime");
            elapsedTime.text = elapsed.ToString();
        }
        // Debug.Log(" End Coroutine");
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        StopCoroutine(timerCoroutine);
        timerCoroutine = null;
    }
}
