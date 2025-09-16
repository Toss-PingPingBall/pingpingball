using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [Header("Coin Settings")]
    public GameObject coinPrefab;  // 동전 프리팹
    public int coinCount = 10;

    [Header("Spawn Area")]
    public Vector2 spawnAreaMin = new Vector2(-3f, -5f); // 좌하단 범위
    public Vector2 spawnAreaMax = new Vector2(3f, 5f);   // 우상단 범위

    void Start()
    {
        SpawnCoins();
    }

    void SpawnCoins()
    {
        if (coinPrefab == null)
        {
            Debug.LogError("Coin Prefab이 할당되지 않았습니다!");
            return;
        }

        for (int i = 0; i < coinCount; i++)
        {
            // 랜덤 위치 생성
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector2 spawnPos = new Vector2(randomX, randomY);

            // 동전 생성
            Instantiate(coinPrefab, spawnPos, Quaternion.identity);
        }
    }
}