using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private bool isUnkillable = false;

    [SerializeField] private bool useCameraShake = false;
    private CameraShake cameraShake;
    private AudioPlayer audioPlayer;

    protected void Awake()
    {
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindFirstObjectByType<AudioPlayer>();
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out DamageDealer damageDealer))
        {
            if (isUnkillable)
            {
                damageDealer.DestroyOrPool();
                return;
            }

            ShakeCamera();
            damageDealer.Hit();
            audioPlayer.PlayDamageClip();
            TakeDamage(damageDealer.GetDamage());
        }
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            audioPlayer.PlayDeathClip();
            Destroy(gameObject);
        }
    }

    private void ShakeCamera()
    {
        if (cameraShake != null && useCameraShake)
            cameraShake.Shake();
    }

    public int GetHealth() => health;
}
