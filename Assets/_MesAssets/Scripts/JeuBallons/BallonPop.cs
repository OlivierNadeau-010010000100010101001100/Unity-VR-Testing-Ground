using UnityEngine;
using System.Collections;

public class BallonPop : MonoBehaviour
{
    [Header("SFX")]
    public AudioClip destructionSound;

    [Range(0f, 1f)]
    public float volume = 1.0f;

    public void Sound()
    {
        if (destructionSound != null)
        {
            AudioSource.PlayClipAtPoint(destructionSound, transform.position, volume);
        }

        gameObject.SetActive(false);
    }
}