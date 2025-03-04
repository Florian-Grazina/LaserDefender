using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Transform Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform[] listCannons;

    [Space(10)]
    [Header("Projectile Settings")]
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFiringRate = 0.5f;

    [Space(10)]
    [Header("AI Settings")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariace = 0f;
    [SerializeField] float minimumFiringRate = 0.1f;

    private Coroutine firingCoroutine;
    [HideInInspector] private int cannonIndex = 0;
    [HideInInspector] public bool IsFiring;

    #region unity methods
    protected void Awake()
    {
        if (listCannons.Length == 0)
            listCannons =  new Transform[] { GetComponent<Transform>() };
    }

    protected void Start()
    {
        if(useAI)
            IsFiring = true;
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
            rb.linearVelocity = transform.up * projectileSpeed;

            cannonIndex = (cannonIndex + 1) % listCannons.Length;
            Destroy(projectile, projectileLifeTime);

            float timeToWait = Random.Range(baseFiringRate - firingRateVariace, baseFiringRate + firingRateVariace);
            timeToWait = Mathf.Clamp(timeToWait, minimumFiringRate, float.MaxValue);

            yield return new WaitForSeconds(timeToWait);
        }
    }
}
