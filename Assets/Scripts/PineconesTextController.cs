using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 火の中にある松ぼっくりの数を表示している text 制御
public class PineconesTextController : MonoBehaviour
{
    // 燃えた松ぼっくりの数
    int burnedPineconesNum = 0;
    [SerializeField] GameObject displayer = null;
    TextMeshProUGUI text;
    BonfireController bonfireController;

    void Start()
    {
        BonfireController.IntoFire += OnHandleIntoFire;
        text = displayer.GetComponent<TextMeshProUGUI>();
        bonfireController = GameObject.FindObjectOfType<BonfireController>();
    }

    // 松ぼっくりが燃やされた時の処理
    void OnHandleIntoFire()
    {
        DisplyBurnedPineconesNum(bonfireController.InFirePineconesNum);
    }
    // 燃やされた松ぼっくりの数を表示
    void DisplyBurnedPineconesNum(int n)
    {
        text.text = n.ToString() + "/3";
    }
}
