using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("Transform Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] listCannons;

    [Space(10)]
    [Header("Projectile Settings")]
    private BulletPoolManager bulletPoolManager;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float baseFiringRate = 0.5f;
    [SerializeField] float minimumFiringRate = 0.05f;
    [HideInInspector] private int cannonIndex = 0;
    [HideInInspector] public bool IsFiring;

    [Space(10)]
    [Header("AI Settings")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariace = 0f;

    private Coroutine firingCoroutine;
    private AudioPlayer audioPlayer;

    #region unity methods
    protected void Awake()
    {
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
        bulletPoolManager = GetComponent<BulletPoolManager>();

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

            GameObject projectile = GetBulletPrefab();
            projectile.transform.position = cannonTransform.position;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.linearVelocity = transform.up * projectileSpeed;

            cannonIndex = (cannonIndex + 1) % listCannons.Length;

            float timeToWait = Random.Range(baseFiringRate - firingRateVariace, baseFiringRate + firingRateVariace);
            timeToWait = Mathf.Clamp(timeToWait, minimumFiringRate, float.MaxValue);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(timeToWait);
        }
    }

    private GameObject GetBulletPrefab()
    {
        if(bulletPoolManager != null)
            return bulletPoolManager.GetNewBulletPrefab();
        else
            return Instantiate(projectilePrefab);
    }
}
