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
            foreach(WaveConfigSO waveConfig in waveConfigs)
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
            Instantiate(currentWave.GetEnemyPrefab(),
                        currentWave.GetStartingWaypoint().position,
                        Quaternion.identity,
                        transform);

            yield return new WaitForSeconds(currentWave.GetRandomSpawnTime());
        }
    }
    #endregion
}
