using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] private float shootingVolume = 1f;
    [SerializeField][Range(0.1f, 3f)] private float shootingPitch = 1f;

    [Header("Damage")]
    [SerializeField] private AudioClip damageClip;
    [SerializeField][Range(0f, 1f)] private float damageVolume = 1f;
    [SerializeField] private AudioClip deathClip;
    [SerializeField][Range(0f, 1f)] private float deathVolume = 1f;
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume, shootingPitch);
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

    private void PlayClip(AudioClip clip, float volume, float pitch)
    {
        if (clip != null)
        {
            GameObject tempAudioSource = new GameObject("TempAudio"); // Temporary object
            AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.pitch = pitch; // Set the pitch
            audioSource.Play();
            Destroy(tempAudioSource, clip.length / pitch); // Destroy object after clip finishes
        }
    }
}