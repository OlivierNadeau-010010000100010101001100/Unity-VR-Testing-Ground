using UnityEngine;

public class BallonPop : MonoBehaviour
{
    [Header("Son à jouer à la destruction")]
    public AudioClip destructionSound;

    [Range(0f, 1f)]
    public float volume = 1.0f;

    void OnDestroy()
    {
        // Vérifie si un clip est assigné
        if (destructionSound != null)
        {
            // Joue le son à la position actuelle de l'objet
            AudioSource.PlayClipAtPoint(destructionSound, transform.position, volume);
        }
    }
}
