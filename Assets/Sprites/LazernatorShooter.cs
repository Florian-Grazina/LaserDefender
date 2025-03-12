using System.Collections;
using UnityEngine;

public class LazernatorShooter : MonoBehaviour
{
    [Header("Transform Settings")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform leftCanon;
    [SerializeField] Transform rightCanon;
    [SerializeField] Transform mainCanon;

    [Space(10)]
    [Header("Projectile Settings")]
    [SerializeField] float baseFiringRate = 5f;
    [SerializeField] float minimumFiringRate = 0.05f;
    [SerializeField] float firingRateVariace = 0f;

    [Space(10)]
    [Header("Laser Settings")]
    [SerializeField] float laserSize = 0.5f;
    [SerializeField] float laserGrowthSpeed = 10f;
    [SerializeField] float maxLaserLength = 5f;

    private Coroutine firingCoroutine;
    [HideInInspector] public bool IsFiring;

    #region unity methods
    protected void Start()
    {
        IsFiring = true;
    }

    protected void Update()
    {
        Fire();
    }
    #endregion

    #region public methods
    private void Fire()
    {
        firingCoroutine ??= StartCoroutine(FireContinuously());
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject projectileMain = Instantiate(projectilePrefab, mainCanon.transform.position, Quaternion.Euler(0, 0, 180));
            projectileMain.transform.SetParent(transform);

            GameObject projectileLeft = Instantiate(projectilePrefab, leftCanon.transform.position, Quaternion.Euler(0, 0, 225));
            projectileLeft.transform.SetParent(transform);

            GameObject projectileRight = Instantiate(projectilePrefab, rightCanon.transform.position, Quaternion.Euler(0, 0, 135));
            projectileRight.transform.SetParent(transform);

            SpriteRenderer mainSprite = projectileMain.GetComponentInChildren<SpriteRenderer>();
            mainSprite.size = new Vector2(laserSize * 2, 0);

            SpriteRenderer leftSprite = projectileLeft.GetComponentInChildren<SpriteRenderer>();
            leftSprite.size = new Vector2(laserSize, 0);

            SpriteRenderer rightSprite = projectileRight.GetComponentInChildren<SpriteRenderer>();
            rightSprite.size = new Vector2(laserSize, 0);

            float currentLength = 0f;
            while (currentLength < maxLaserLength)
            {
                currentLength += laserGrowthSpeed * Time.deltaTime;
                mainSprite.size = new Vector2(laserSize * 2, currentLength);
                leftSprite.size = new Vector2(laserSize, currentLength);
                rightSprite.size = new Vector2(laserSize, currentLength);
                yield return null;
            }

            float timeToWait = Random.Range(baseFiringRate - firingRateVariace, baseFiringRate + firingRateVariace);
            timeToWait = Mathf.Clamp(timeToWait, minimumFiringRate, float.MaxValue);

            yield return new WaitForSeconds(timeToWait);

            Destroy(projectileMain);
            Destroy(projectileLeft);
            Destroy(projectileRight);
        }
    }
    #endregion
}
