using System.Collections;
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

    public void DestroyDestroyPrefab(GameObject destroyObject, float delay)
    {
        StartCoroutine(DelayDestroy(destroyObject, delay));
    }

    private void CreateDestroyFrefab()
    {
        GameObject newObject = Instantiate(destroyPrefab);
        destroyAvailableObjects.Add(newObject);
    }

    private IEnumerator DelayDestroy(GameObject destroyObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        destroyObject.SetActive(false);
        destroyAvailableObjects.Add(destroyObject);
    }
}
