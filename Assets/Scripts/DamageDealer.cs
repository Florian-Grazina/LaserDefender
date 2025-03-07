using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float maxDistance = 10f;
    private BulletPoolManager poolManager;
    private Vector2 startPosition;

    protected void Awake()
    {
        startPosition = transform.position;
    }

    protected void Update()
    {
        if(Vector2.Distance(startPosition, transform.position) > maxDistance)
            DestroyOrPool();
    }

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

    public void DestroyOrPool()
    {
        if(poolManager != null)
            poolManager.DestroyBulletPrefab(gameObject);
        else
            Destroy(gameObject);
    }
}
