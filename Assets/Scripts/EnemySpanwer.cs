using UnityEngine;

public class EnemySpanwer : MonoBehaviour
{
    [SerializeField] private WaveConfigSO currentWave;

    protected void Start()
    {
        SpawnEnemies();
    }

    public WaveConfigSO GetCurrentWave() => currentWave;

    private void SpawnEnemies()
    {
        for (int i = 0; i < currentWave.GetWaveSize(); i++)
        {
            Instantiate(currentWave.GetEnemyPrefab(),
                    currentWave.GetStartingWaypoint().position,
                    Quaternion.identity,
                    transform);
        }
    }
}
