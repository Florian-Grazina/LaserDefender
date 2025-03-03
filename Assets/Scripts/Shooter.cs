using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float fireRate = 0.5f;

    private Coroutine firingCoroutine;
    public bool IsFiring;

    #region unity methods
    protected void Start()
    {
        
    }

    protected void Update()
    {
        Fire();
    }
    #endregion

    private void Fire()
    {
        if(IsFiring)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else
        {
            if(firingCoroutine != null)
                StopCoroutine(firingCoroutine);
        }
    }

    private IEnumerator FireContinuously()
    {
        if (IsFiring)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.up * projectileSpeed;
            Destroy(projectile, projectileLifeTime);

            yield return new WaitForSeconds(fireRate);
        }
    }
}
