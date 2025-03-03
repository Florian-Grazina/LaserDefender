using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    [Header("Wave")]
    [SerializeField] List<WaveConfigSO> waveConfigs;
    [SerializeField] float timeBetweenWaves = 0f;
    private WaveConfigSO currentWave;

    [SerializeField] private bool isLooping;

    protected void Start()
    {
        StartCoroutine(StartNextWave());
    }

    public WaveConfigSO GetCurrentWave() => currentWave;

    #region spwan logic
    private IEnumerator StartNextWave()
    {
        do
        {
            foreach (WaveConfigSO waveConfig in waveConfigs)
            {
                currentWave = waveConfig;
                yield return StartCoroutine(SpawnEnemies());
                yield return new WaitForSeconds(timeBetweenWaves);
            }
        }
        while (isLooping);
    }

    private IEnumerator SpawnEnemies()
    {
        var wayPoints = currentWave.GetWaypoints();
        var mirroredWayPoints = currentWave.GetWaypoints(true);

        for (int i = 0; i < currentWave.GetWaveSize(); i++)
        {
            SpawnEnemy(wayPoints);

            if (currentWave.GetIsMirroredWave())
                SpawnEnemy(mirroredWayPoints);

            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
        }
    }

    private void SpawnEnemy(List<Vector2> wayPoints)
    {
        GameObject enemy = Instantiate(currentWave.GetEnemyPrefab(),
                                       wayPoints[0],
                                       Quaternion.identity,
                                       transform);

        Pathfinder pathFinder = enemy.GetComponent<Pathfinder>();
        pathFinder.SetPathFindingSettings(currentWave.GetMoveSpeed(), wayPoints);
    }
    #endregion
}
