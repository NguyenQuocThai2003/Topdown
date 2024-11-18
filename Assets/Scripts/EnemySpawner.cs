using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Quái Prefab")]
    public GameObject[] enemyPrefabs; // Mảng chứa các Prefab quái
    public Transform[] spawnPoints;  // Các điểm spawn

    [Header("Hiệu ứng spawn")]
    public GameObject spawnEffectPrefab; // Prefab hiệu ứng spawn
    public List<Transform> specialSpawnPoints; // Danh sách các SpawnPoint đặc biệt

    [Header("Cài đặt Spawn")]
    public float spawnInterval = 2f; // Thời gian giữa mỗi lần spawn
    public int maxEnemies = 10;      // Số lượng quái tối đa

    private int currentEnemies = 0;  // Quái hiện tại trong Scene

    void Start()
    {
        // Bắt đầu quá trình spawn
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (currentEnemies < maxEnemies)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Lấy Prefab ngẫu nhiên từ mảng enemyPrefabs
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            // Lấy một điểm spawn ngẫu nhiên
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Kiểm tra nếu SpawnPoint là "đặc biệt" (có trong danh sách)
            if (specialSpawnPoints.Contains(randomSpawnPoint) && spawnEffectPrefab != null)
            {
                GameObject spawnEffect = Instantiate(spawnEffectPrefab, randomSpawnPoint.position, Quaternion.identity);
                Destroy(spawnEffect, 2f); // Hủy hiệu ứng sau 2 giây
            }

            // Spawn quái
            Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);

            currentEnemies++;
        }
    }

}
