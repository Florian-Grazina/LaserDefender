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

    public Transform GetStartingWaypoint(bool isMirrored = false)
    {
        if(isMirrored)
            return GetWaypoints()[0];
        return GetWaypoints()[0];
    }

    public List<Transform> GetWaypoints(bool isMirrored = false)
    {
        var waypoints = pathPrefab.Cast<Transform>().ToList();

        if (!isMirrored)
            return waypoints;

        var mirroredWaypoints = new List<Transform>();
        foreach (var waypoint in waypoints)
        {
            GameObject dummy = new ("MirroredWaypoint");
            dummy.transform.position = new Vector3(-waypoint.position.x, waypoint.position.y, waypoint.position.z);
            mirroredWaypoints.Add(dummy.transform);
        }

        return mirroredWaypoints;
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
