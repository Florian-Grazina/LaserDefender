using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private List<Transform> waypoints;
    private int waypointIndex = 0;
    private float moveSpeed;

    #region unity methods
    protected void Awake()
    {
    }

    protected void Start()
    {
    }

    protected void Update()
    {
        FollowPath();
    }
    #endregion

    #region pathfinder logic
    public void SetPathFindingSettings(float speed, List<Transform> listWayPoints)
    {
        moveSpeed = speed;
        waypoints = listWayPoints;
        transform.position = waypoints[0].position;
    }

    private void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float deltaMovement = moveSpeed * Time.deltaTime;
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
