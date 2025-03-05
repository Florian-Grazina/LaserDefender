using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    private BulletPoolManager poolManager;

    public int GetDamage() => damage;

    public void Initialize(BulletPoolManager manager)
    {
        poolManager = manager;
    }

    public void Hit()
    {
        GameObject destroyPrefab = DamageAnimationPoolManager.Instance.GetNewDamagePrefab();
        destroyPrefab.transform.position = transform.position;

        DestroyOrPool();
    }

    private void DestroyOrPool()
    {
        if(poolManager != null)
            poolManager.DestroyBulletPrefab(gameObject);
        else
            Destroy(gameObject);
    }
}
