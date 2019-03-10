using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs;

    private Transform playerTransform;  // 플레이어 위치를 기준으로 새 타일을 생성시킬지 정하기 위해 플레이어위치 변수
    private float spawnZ = -6.0f;   // 어느 z좌표에 오브젝트 생성시킬지, -6으로 한 것은 시작 시 자연스럽게 하기 위해서임
    private float tileLength = 12.0f;   // 오브젝트(타일) z 길이
    private int amnTilesOnScreen = 7;   // 몇 개의 타일을 게임에 넣을 건지
    private float safeZone = 30.0f;     // 시작하자마자 타일 삭제되는것 방지
    private int lastPrefabIndex = 0;    // 타일 종류 다양하게 나타내기 위함
    private bool isDead = false;



    private List<GameObject> activeTiles;   // 어떤 타일이 게임 상에 있는지 알기 위해서는 필요(삭제 시 특히 필요)

    // Use this for initialization
    private void Start()
    {
        activeTiles = new List<GameObject>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; //태그로 플레이어 위치 찾기

        for (int i = 0; i < amnTilesOnScreen; i++)   // start에서 먼저 7개 타일 생성한다
        {
            if (i < 2)  // 초반에는 똑같은 프리팹만 생성
                SpawnTile(0);
            else
                SpawnTile();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isDead)
            return;

        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))  // spawnZ에서 타일개수*타일길이 뺀것보다 크면 생성, 삭제
        {
            SpawnTile();
            DeleteTile();   //타일딜리트해준다
        }
    }

    private void SpawnTile(int prefabIndex = -1)    // 타일 생성 함수이다
    {
        GameObject go;
        if (prefabIndex == -1)
        {
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;   // 랜덤하게 프리팹 생성
        }
        else
            go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;   // 처음에는 한가지 종류의 프리팹만 생성
        go.transform.SetParent(transform);  // tilemanager 오브젝트에 생성시킴
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;   // 타일하나 생성하면 spawnz 타일길이만큼 증가시킴
        activeTiles.Add(go);    // 타일 오브젝트 배열에 저장한다
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex() // 타일 프리팹 랜덤하게 바꿔 주는 함수
    {
        if (tilePrefabs.Length <= 1)    // 타일 프리팹이 하나이면 0 리턴한다
            return 0;
        int randomIndex = lastPrefabIndex;  // 랜덤인덱스 = 최근프리팹인덱스
        while (randomIndex == lastPrefabIndex)  // 랜덤인덱스가 최근프리팹인덱스와 같은 동안
        {
            randomIndex = Random.Range(1, tilePrefabs.Length);  // 임의로 타일프리팹 번호 선정
        }
        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
    public void OnDeath()
    {
        isDead = true;
    }
}