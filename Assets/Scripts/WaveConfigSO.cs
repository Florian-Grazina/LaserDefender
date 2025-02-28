using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [Header("Path")]
    [SerializeField] private Transform pathPrefab;

    [Space(10)]
    [Header("Enemy")]
    [SerializeField] private int waveSize = 5;
    [SerializeField] private GameObject enemyPrefabs;
    [SerializeField] private float moveSpeed = 5f;
    
    [Space(10)]
    [Header("Spawn")]
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnTimeVariance = 0.5f;
    [SerializeField] private float minimumSpawnTime = 0.2f;

    #region getters
    public GameObject GetEnemyPrefab() => enemyPrefabs;

    public int GetWaveSize() => waveSize;

    public Transform GetStartingWaypoint() => pathPrefab.GetChild(0);

    public List<Transform> GetWaypoints() => pathPrefab.Cast<Transform>().Select(x => x).ToList();

    public float GetMoveSpeed() => moveSpeed;
    #endregion

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(spawnInterval - spawnTimeVariance, spawnInterval + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
