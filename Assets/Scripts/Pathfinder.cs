using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    EnemySpanwer enemySpanwer;
    private WaveConfigSO waveConfigSO;
    private List<Transform> waypoints;
    private int waypointIndex = 0;

    #region unity methods
    protected void Awake()
    {
        enemySpanwer = FindFirstObjectByType<EnemySpanwer>();
    }

    protected void Start()
    {
        waveConfigSO = enemySpanwer.GetCurrentWave();
        waypoints = waveConfigSO.GetWaypoints();
        transform.position = waypoints[0].position;
    }

    protected void Update()
    {
        FollowPath();
    }
    #endregion

    #region private methods
    private void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float deltaMovement = waveConfigSO.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, deltaMovement);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
