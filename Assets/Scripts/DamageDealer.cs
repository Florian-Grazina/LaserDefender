using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private GameObject destroyEffectPrefab;

    public int GetDamage() => damage;

    public void Hit()
    {
        Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
