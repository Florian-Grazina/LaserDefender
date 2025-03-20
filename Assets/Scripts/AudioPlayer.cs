using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] private float shootingVolume = 1f;

    [Header("Damage")]
    [SerializeField] private AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] private float damageVolume = 1f;
    [SerializeField] private AudioClip deathClip;
    [SerializeField][Range(0f, 1f)] private float deathVolume = 1f;
    public void PlayShootingClip()
    {
        if (shootingClip != null)
            AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, shootingVolume);
    }

    public void PlayDamageClip()
    {
        if (damageClip != null)
            AudioSource.PlayClipAtPoint(damageClip, Camera.main.transform.position, damageVolume);
    }

    public void PlayDeathClip()
    {
        if (deathClip != null)
            AudioSource.PlayClipAtPoint(deathClip, Camera.main.transform.position, deathVolume);
    }
}