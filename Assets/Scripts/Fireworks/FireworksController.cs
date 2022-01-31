using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// セットしてある Firework を打ち上げるクラス
public class FireworksController : MonoBehaviour
{
    [SerializeField] List<GameObject> fireworks = null;
    Coroutine shootCoroutine;

    [SerializeField] GameObject flash = null;               // 花火の閃光を再現するもの
    [SerializeField] GameObject flashPostProcessing = null; // 花火の閃光時の描画効果を再現するもの

    // 花火を打ち上げるメソッド
    public void ShootFireworks()
    {
        shootCoroutine = StartCoroutine(ShootRoutine());
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            ShootFireworks();
        }
    }

    // セットされている花火を生成
    public IEnumerator ShootRoutine()
    {
        // yield return null;
        int i = 0;
        while(fireworks.Count > i)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.0f));
            fireworks[i].GetComponent<ParticleSystem>().Play();
            i++;
        }
        yield return new WaitForSeconds(1.8f);
        flashPostProcessing.SetActive(true);
        flash.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        flash.SetActive(false);
        flashPostProcessing.SetActive(false);
        StopCoroutine(shootCoroutine);
        shootCoroutine = null;
        // Destroy(this.gameObject);
    }
}
