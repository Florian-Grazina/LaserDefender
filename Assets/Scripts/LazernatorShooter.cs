using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] float laserActiveTime = 2f;
    [SerializeField] float firingRateVariace = 0f;
    [SerializeField] float laserCooldown = 2f;

    [Space(10)]
    [Header("Laser Settings")]
    [SerializeField] float laserSize = 0.5f;
    [SerializeField] float laserGrowthSpeed = 10f;
    [SerializeField] float maxLaserLength = 5f;

    [Space(10)]
    [Header("Laser Noise Settings")]
    [SerializeField] float noiseAmplitude = 0.2f;
    [SerializeField] float noiseFrequency = 10f;

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
            List<GameObject> projectiles = new(){
                CreateLaser(mainCanon, laserSize ),
                CreateLaser(leftCanon, laserSize),
                CreateLaser(rightCanon, laserSize)
            };

            AddLasersNoises(projectiles);
            yield return GrowLasers(projectiles);

            yield return new WaitForSeconds(laserActiveTime);

            projectiles.ForEach(Destroy);

            float timeToWait = Random.Range(laserCooldown - firingRateVariace, laserCooldown + firingRateVariace);
            yield return new WaitForSeconds(timeToWait);
        }
    }

    private GameObject CreateLaser(Transform canon, float laserSize)
    {
        GameObject projectile = Instantiate(projectilePrefab, canon.position, Quaternion.identity);
        projectile.transform.SetParent(canon);
        projectile.transform.localRotation = Quaternion.identity;

        SpriteRenderer sprite = projectile.GetComponentInChildren<SpriteRenderer>();
        sprite.size = new Vector2(laserSize, 0);

        return projectile;
    }

    private void AddLasersNoises(List<GameObject> projectiles)
    {
        foreach (var projectile in projectiles)
            StartCoroutine(AddLaserNoise(projectile.transform));
    }

    private IEnumerator AddLaserNoise(Transform laserTransform)
    {
        Vector3 initialPosition = laserTransform.localPosition;

        while (laserTransform != null)
        {
            float noise = Mathf.PerlinNoise(Time.time * noiseFrequency, 0f) * 2f - 1f;
            laserTransform.localPosition = initialPosition + noise * noiseAmplitude * laserTransform.right;

            yield return null;
        }
    }

    private IEnumerator GrowLasers(List<GameObject> lasers)
    {
        float currentLength = 0f;
        SpriteRenderer[] sprites = lasers.Select(laser => laser.GetComponentInChildren<SpriteRenderer>()).ToArray();
        BoxCollider2D[] colliders = lasers.Select(laser => laser.GetComponent<BoxCollider2D>()).ToArray();

        while (currentLength < maxLaserLength)
        {
            currentLength += laserGrowthSpeed * Time.deltaTime;

            for (int i = 0; i < sprites.Length; i++)
            {
                var sprite = sprites[i];
                var collider = colliders[i];

                sprite.size = new Vector2(sprite.size.x, currentLength);
                collider.size = new Vector2(collider.size.x, currentLength);
                //collider.offset = new Vector2(collider.offset.x, currentLength / 2f); // Center the collider on the growing laser
            }

            yield return null;
        }
    }

    #endregion
}
