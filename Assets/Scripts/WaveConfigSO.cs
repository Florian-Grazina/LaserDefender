using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfigSO : ScriptableObject
{
    [SerializeField] private GameObject enemyPrefabs;
    [SerializeField] private Transform pathPrefab;
    [SerializeField] private int waveSize = 5;
    [SerializeField] private float moveSpeed = 5f;


    #region public methods
    public GameObject GetEnemyPrefab() => enemyPrefabs;

    public int GetWaveSize() => waveSize;

    public Transform GetStartingWaypoint() => pathPrefab.GetChild(0);

    public List<Transform> GetWaypoints() => pathPrefab.Cast<Transform>().Select(x => x).ToList();

    public float GetMoveSpeed() => moveSpeed;
    #endregion
}
