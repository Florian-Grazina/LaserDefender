using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private float moveSpeed = 5f;


    #region public methods
    public int GetNumberOfEnemies() => enemyPrefabs.Count;

    public GameObject GetEnemyPrefab(int index) => enemyPrefabs[index];

    public Transform GetStartingWaypoint() => pathPrefab.GetChild(0);

    public List<Transform> GetWaypoints() => pathPrefab.Cast<Transform>().Select(x => x).ToList();

    public float GetMoveSpeed() => moveSpeed;
    #endregion
}
