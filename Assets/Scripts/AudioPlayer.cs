using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] private AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] private float shootingVolume = 1f;

    public void PlayShootingClip()
    {
        if (shootingClip != null)
            AudioSource.PlayClipAtPoint(shootingClip, Camera.main.transform.position, shootingVolume);
    }
}