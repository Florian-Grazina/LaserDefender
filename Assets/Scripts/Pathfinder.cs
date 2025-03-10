using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private List<Vector2> waypoints;
    private int waypointIndex = 0;
    private float moveSpeed;
    private bool isBoss;

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
    public void SetPathFindingSettings(float speed, List<Vector2> listWayPoints, bool isBossBool)
    {
        moveSpeed = speed;
        waypoints = listWayPoints;
        transform.position = waypoints[0];
        isBoss = isBossBool;
    }

    private void FollowPath()
    {
        if (waypointIndex < waypoints.Count)
        {
            Vector3 targetPosition = waypoints[waypointIndex];
            float deltaMovement = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, deltaMovement);

            if (transform.position == targetPosition)
            {
                waypointIndex++;

                if (waypointIndex == waypoints.Count && isBoss)
                    waypointIndex = 0;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
}
