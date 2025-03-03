using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform[] listCannons;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float firingRate = 0.5f;

    private Coroutine firingCoroutine;
    private int cannonIndex = 0;
    public bool IsFiring;

    #region unity methods
    protected void Awake()
    {
        if (listCannons.Length == 0)
            listCannons =  new Transform[] { GetComponent<Transform>() };
    }

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
        if(IsFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!IsFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            Transform cannonTransform = listCannons[cannonIndex];

            GameObject projectile = Instantiate(projectilePrefab, cannonTransform.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = Vector2.up * projectileSpeed;

            cannonIndex = (cannonIndex + 1) % listCannons.Length;

            Debug.Log(cannonIndex);

            Destroy(projectile, projectileLifeTime);
            yield return new WaitForSeconds(firingRate);
        }
    }
}
