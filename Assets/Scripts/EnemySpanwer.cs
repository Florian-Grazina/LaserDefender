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
        for (int i = 0; i < currentWave.GetWaveSize(); i++)
        {
            GameObject enemy = Instantiate(currentWave.GetEnemyPrefab(),
                                            currentWave.GetStartingWaypoint(),
                                            Quaternion.identity,
                                            transform);

            Pathfinder pathfinder = enemy.GetComponent<Pathfinder>();
            pathfinder.SetPathFindingSettings(currentWave.GetMoveSpeed(), currentWave.GetWaypoints());

            if (currentWave.GetIsMirroredWave())
            {
                Instantiate(currentWave.GetEnemyPrefab(),
                            currentWave.GetStartingWaypoint(true),
                            Quaternion.identity,
                            transform);

                pathfinder = enemy.GetComponent<Pathfinder>();
                pathfinder.SetPathFindingSettings(currentWave.GetMoveSpeed(), currentWave.GetWaypoints(true));
            }

            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
        }
    }
    #endregion
}
