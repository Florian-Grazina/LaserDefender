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
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
       PlayClip(damageClip, damageVolume);
    }

    public void PlayDeathClip()
    {
       PlayClip(deathClip, deathVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}