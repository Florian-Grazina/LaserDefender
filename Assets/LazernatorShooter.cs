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

    [Space(10)]
    [Header("AI Settings")]
    [SerializeField] bool useAI;
    [SerializeField] float firingRateVariace = 0f;
    [SerializeField] float firingRange = 0f;

    private Coroutine firingCoroutine;
    [HideInInspector] private int cannonIndex = 0;
    [HideInInspector] public bool IsFiring;

}
