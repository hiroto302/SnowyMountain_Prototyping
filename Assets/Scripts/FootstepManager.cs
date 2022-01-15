using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// 足音を鳴らすスクリプト
// terrain の地表テクスチャに対応した足音がなるように実装する
public class FootstepManager : MonoBehaviour
{
    [SerializeField]
    AudioSource source;
    // 各地面に適応した地面の足音
    // オーディオエフェクトの繰り返しは、いずれ必ず気付かれ、かなり煩わしくなる可能性がある
    // 人は一歩一歩、足の置き方が変わるので、音も大きく変わってきます。何百種類もの足音音声を用意するのは無理だが、
    // ほんの少しであれば、繰り返しの効果に気づかれないこともある。なのでバリエーションを増やすために List 型。
    public List<AudioClip> snowSteps = new List<AudioClip>();
    public List<AudioClip> sandSteps = new List<AudioClip>();
    public List<AudioClip> waterSteps = new List<AudioClip>();

    // 地表の変数
    enum Surface {snow, sand, water};
    Surface surface;
    // 現在playerが立っている地表に対応した複数の足音 AudioClip の list を格納
    List<AudioClip> currentStepsList;


    // terrain で地表を判断して音を鳴らすための変数群
    Terrain terrain;
    TerrainData terrainData;

    // editor 上でパラメータを編集できる AudioClips クラス : タグに対応した audioClip の list を持たせる
    [System.Serializable]
    public class AudioClips
    {
        public string groundTypeTag;        // 地表の種類名
        public AudioClip[] audioClips;      // 各地表で鳴る音
    }
    // AudioClipsクラスのList : 足音の種類毎にタグ名とオーディオクリップを登録する
    [SerializeField]
    List<AudioClips> listAudioClips = new List<AudioClips>();
    // Terrain Layers と 足音判定用タグの対応関係を記入する。 0番目 : Snow, 1番目 : Sand
    [SerializeField]
    string[] terrainLayerToTag;
    // key : 地表, int : レイヤーインデックス
    Dictionary<string, int> tagToIndex = new Dictionary<string, int>();
    // // 現在、Playerが位置するterrainのレイヤーインデックス
    int groundIndex = 0;

    void Awake()
    {
        // tagToIndex に 番号を割り当て
        for(int i = 0; i < listAudioClips.Count; ++i)
        {
            // tagToIndex の keyに, listAudioClips に登録されている i番目の 地表名, value に i を追加。 タグ名と番号を関連付ける
            tagToIndex.Add(listAudioClips[i].groundTypeTag, i);
        }
    }
    void Start()
    {
        // main terrain を取得
        terrain = Terrain.activeTerrain;
        // Terrain Data	: ハイトマップ、Terrain のテクスチャ、ディテールメッシュ、Tree を格納する TerrainData アセット。
        // ここから、テクスチャの情報を取得したい
        terrainData = terrain.terrainData;
        // Debug.Log(terrain + " : terrain");         // Terrain_0_0_c76ae34e-ea64-4a40-b106-7182d18cc6bc
        // Debug.Log(terrainData + " : terrainData"); // Terrain_0_0_SnowyMountain
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            PlayFootstepSE();
        }
    }

    // 足音を鳴らすメソッド
    // 任意のタイミングでならそう
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
        // タグを利用した方法
        // 水面であるか判定
        // if(other.gameObject.tag == "Water")
        // {
        //     surface = Surface.water;
        //     SelectStepList();
        // }
        if(tagToIndex.ContainsKey(other.gameObject.tag))
            groundIndex = tagToIndex[other.gameObject.tag];

        // terrainの地表の種類を判定す方法
        // タグを取得したい時(terrain で作成されていない waterなど)
        if(tagToIndex.ContainsKey(other.gameObject.tag))
            groundIndex = tagToIndex[other.gameObject.tag];

        // 接触したobjectが main terrain の時
        if(other.gameObject.GetInstanceID() == terrain.gameObject.GetInstanceID())
        {
            // terrain から現在値の Alphamap を取得
            // player が terrain の何処に位置するか
            Vector3 position = transform.position - terrain.transform.position;
            // GetAlphamaps :(x, y, width, height)を設定し、てアルファマップを取得
            // 返される配列は三次元。 最初のふたつは 地図上の座標 x と y を表し、3 番目は alphamap を適用する splatmap テクスチャを表す。
            // alphamapWidth, Hight は、のTexture Resolutions項目で、Control Texture Resolution で設定したもの (ここでは, 512 * 512)
            // terrainData.size は Mesh Resolutionsで確認できる。 terrain オブジェクトを作成した時のサイズ(100, 100)
            // 下記の式より, ワールド単位におけるPlayer が位置する alphamap の 情報を取得することが可能となる
            int offsetX = (int)(terrainData.alphamapWidth / terrainData.size.x * position.x);
            int offsetZ = (int)(terrainData.alphamapHeight / terrainData.size.z * position.z);
            float[,,] alphamaps = terrainData.GetAlphamaps(offsetX, offsetZ, 1, 1);
            // Alphamap中で成分が最大のTerrainLayer を探す
            // alphamapsから 各成分(Layer Paletteに登録されいるもの。ここでは, 0番目 snow, 1番目 dirt)を格納. 全体を１時の各成分の割合がわかる。
            float[] weights = alphamaps.Cast<float>().ToArray();
            // 最大成分を取得
            // IndexoOf 指定した配列から、値が最も高い要素のインデックスを習得
            int terrainLayer = System.Array.IndexOf(weights, weights.Max());
            // もし、 terrainLayer が 1(砂) の時, tagToIndex の sand に登録してある value １ を取得する
            // 現在踏んでいる地面の種類番号を取得
            groundIndex = tagToIndex[terrainLayerToTag[terrainLayer]];
        }
        Debug.Log(groundIndex);
    }

    public void PlayFootstepSE()
    {
        // groundIndexで呼び出すオーディオクリップを変える
        AudioClip[] clips = listAudioClips[groundIndex].audioClips;
        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

}

