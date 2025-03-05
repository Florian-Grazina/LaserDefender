using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoBehaviour
{
    [SerializeField] private int poolSize = 20;
    [SerializeField] private GameObject bulletPrefab;
    private Queue<GameObject> bulletAvailableObjects = new();

    protected void Awake()
    {
        for (int i = 0; i < poolSize; i++)
            CreateBulletPrefab();
    }

    public GameObject GetNewBulletPrefab()
    {
        GameObject returnObject;
        if (bulletAvailableObjects.Count == 0)
            CreateBulletPrefab();

        returnObject = bulletAvailableObjects.Dequeue();
        returnObject.SetActive(true);
        return returnObject;
    }

    public void DestroyBulletPrefab(GameObject destroyObject)
    {
        destroyObject.SetActive(false);
        bulletAvailableObjects.Enqueue(destroyObject);
    }

    private void CreateBulletPrefab()
    {
        GameObject bullet = Instantiate(bulletPrefab);
        bullet.SetActive(false);
        bullet.GetComponent<DamageDealer>().Initialize(this);
        bulletAvailableObjects.Enqueue(bullet);
    }
}
