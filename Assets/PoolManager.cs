using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] private GameObject destroyPrefab;
    private List<GameObject> destroyAvailableObjects = new();

    public GameObject GetNewDestroyPrefab()
    {
        GameObject returnObject;
        if (destroyAvailableObjects.Count == 0)
            CreateDestroyFrefab();

        returnObject = destroyAvailableObjects[0];
        returnObject.SetActive(true);
        destroyAvailableObjects.Remove(returnObject);
        return returnObject;
    }

    private void DestroyDestroyPrefab(GameObject destroyObject)
    {
        destroyObject.SetActive(false);
        destroyAvailableObjects.Add(destroyObject);
    }

    private void CreateDestroyFrefab()
    {
        GameObject newObject = Instantiate(destroyPrefab);
        destroyAvailableObjects.Add(newObject);
    }
}
