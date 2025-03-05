using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private bool isUnkillable = false;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageDealer damageDealer))
        {
            damageDealer.Hit();
            TakeDamage(damageDealer.GetDamage());
        }
    }

    private void TakeDamage(int damage)
    {
        if(isUnkillable)
            return;

        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
