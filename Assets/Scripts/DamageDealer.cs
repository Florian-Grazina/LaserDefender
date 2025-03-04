using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float destroyDelay = 0.5f;

    public int GetDamage() => damage;

    public void Hit()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        Destroy(gameObject, destroyDelay);
        GetComponent<Animator>().SetTrigger("Destroy");
    }
}
