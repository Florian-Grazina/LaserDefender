using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;

    public int GetDamage() => damage;

    public void Hit()
    {
        GameObject destroyPrefab = FindFirstObjectByType<PoolManager>().GetNewDestroyPrefab();
        destroyPrefab.transform.position = transform.position;

        Destroy(gameObject);
    }
}
