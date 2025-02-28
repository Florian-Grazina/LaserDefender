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
        Instantiate(currentWave.GetEnemyPrefab(0),
                    currentWave.GetStartingWaypoint().position,
                    Quaternion.identity);

    }
}
