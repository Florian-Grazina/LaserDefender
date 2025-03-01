using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [Header("Path")]
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private bool isMirroredWave;

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

    public Vector2 GetStartingWaypoint(bool isMirrored = false)
    {
        if(isMirrored)
            return GetWaypoints()[0];
        return GetWaypoints()[0];
    }

    public List<Vector2> GetWaypoints(bool isMirrored = false)
    {
        var waypoints = pathPrefab.Cast<Transform>()
                                  .Select(waypoint => (Vector2)waypoint.position)
                                  .ToList();

        if (isMirrored)
        {
            waypoints = waypoints.Select(wp => new Vector2(-wp.x, wp.y)).ToList();
            Debug.Log(waypoints.Count);
        }

        return waypoints;
    }

    public float GetMoveSpeed() => moveSpeed;

    public bool GetIsMirroredWave() => isMirroredWave;
    #endregion

    public float GetRandomSpawnTime()
    {
        float spawnTime = Random.Range(spawnInterval - spawnTimeVariance, spawnInterval + spawnTimeVariance);
        return Mathf.Clamp(spawnTime, minimumSpawnTime, float.MaxValue);
    }
}
