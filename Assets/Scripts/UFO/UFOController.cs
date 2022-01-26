using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Timeline 終了後、UFO が落下した後の処理を書く
public class UFOController : MonoBehaviour
{
    // 落下したUFO
    [SerializeField] GameObject fallenUFO = null;
    // 落下前の通常の木
    [SerializeField] GameObject treesParent = null;
    // 落下後の壊された木
    [SerializeField] GameObject brokenTreesParent = null;
    // 乗っているエイリアン
    [SerializeField] List<GameObject> aliens = null;

    void Start()
    {
        TimelineDirector.OnFallUFO += HandleFallenUFO;
    }
    // UFOが墜落した時に行う処理
    void HandleFallenUFO()
    {
        ChangeEnvironmentObject();
        AppearAliens();
    }

    //  環境オブジェクトの更新
    void ChangeEnvironmentObject()
    {
        fallenUFO.SetActive(true);
        treesParent.SetActive(false);
        brokenTreesParent.SetActive(true);
    }
    // エイリアンを出現する
    void AppearAliens()
    {
        foreach(GameObject alien in aliens)
        {
            alien.SetActive(true);
        }
    }
}
