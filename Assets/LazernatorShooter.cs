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
    [SerializeField] float baseFiringRate = 0.5f;
    [SerializeField] float minimumFiringRate = 0.05f;
    [SerializeField] float firingRateVariace = 0f;

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
            GameObject projectileLeft = Instantiate(projectilePrefab);
            projectileLeft.transform.position = leftCanon.transform.position;

            GameObject projectileRight = Instantiate(projectilePrefab);
            projectileRight.transform.position = rightCanon.transform.position;

            GameObject projectileMain = Instantiate(projectilePrefab);
            projectileMain.transform.position = mainCanon.transform.position;

            float timeToWait = Random.Range(baseFiringRate - firingRateVariace, baseFiringRate + firingRateVariace);
            timeToWait = Mathf.Clamp(timeToWait, minimumFiringRate, float.MaxValue);

            yield return new WaitForSeconds(timeToWait);
        }
    }
    #endregion
}
