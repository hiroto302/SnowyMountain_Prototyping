using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// セットしてある Firework を打ち上げるクラス
public class FireworksController : MonoBehaviour
{
    [SerializeField] List<GameObject> fireworks = null;
    Coroutine shootCoroutine;
    // 花火を打ち上げるメソッド
    public void ShootFireworks()
    {
        shootCoroutine = StartCoroutine(ShootRoutine());
    }

    // セットされている花火を生成
    public IEnumerator ShootRoutine()
    {
        yield return null;
        int i = 0;
        while(fireworks.Count > i)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.0f));
            fireworks[i].GetComponent<ParticleSystem>().Play();
            i++;
        }
        StopCoroutine(shootCoroutine);
        shootCoroutine = null;
        // Destroy(this.gameObject);
    }
}
