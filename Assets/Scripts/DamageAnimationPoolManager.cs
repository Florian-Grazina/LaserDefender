using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAnimationPoolManager : MonoBehaviour
{
    [SerializeField] private int poolSize = 10;
    [SerializeField] private GameObject damageAnimationPrefab;
    private Queue<GameObject> damageAvailableGameObject = new();

    public static DamageAnimationPoolManager Instance { get; private set; }

    protected void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        for (int i = 0; i < poolSize; i++)
            CreateDamagePrefab();
    }

    public GameObject GetNewDamagePrefab()
    {
        GameObject returnObject;
        if (damageAvailableGameObject.Count == 0)
            CreateDamagePrefab();

        returnObject = damageAvailableGameObject.Dequeue();
        returnObject.SetActive(true);
        return returnObject;
    }

    public void DestroyDamageAnimationPrefab(GameObject destroyObject, float delay)
    {
        StartCoroutine(DelayDestroy(destroyObject, delay));
    }

    private IEnumerator DelayDestroy(GameObject destroyObject, float delay)
    {
        yield return new WaitForSeconds(delay);
        destroyObject.SetActive(false);
        damageAvailableGameObject.Enqueue(destroyObject);
    }

    private void CreateDamagePrefab()
    {
        GameObject bullet = Instantiate(damageAnimationPrefab);
        bullet.SetActive(false);
        damageAvailableGameObject.Enqueue(bullet);
    }
}
