using System.Collections;
using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    [SerializeField] private WaveConfigSO currentWave;

    protected void Start()
    {
        SpawnEnemies();
    }

    public WaveConfigSO GetCurrentWave() => currentWave;

    #region spwan logic
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
