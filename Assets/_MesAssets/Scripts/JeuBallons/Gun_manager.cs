using UnityEngine;
using UnityEngine.InputSystem; // Nécessaire si tu utilises le nouveau Input System (recommandé en VR)

public class VRGunManager : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private Transform gunPoint;         // Transform du canon ou main
    [SerializeField] private string targetTag = "Ballons"; // Tag à détecter

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;

    public void Shoot()
    {
        if (audioSource && shootClip)
        {
            audioSource.PlayOneShot(shootClip);
        }

        Vector3 origin = gunPoint.position;
        Vector3 direction = gunPoint.forward;
        float debugLength = 10f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, Mathf.Infinity))
        {

            if (hit.collider.CompareTag(targetTag))
            {
                Debug.DrawRay(origin, direction * debugLength, Color.green, 1f);
                Debug.Log($"[VR] Cible touchée : {hit.collider.name}");

                // destruction de la  balloon touché
                Destroy(hit.collider.gameObject);

                if (BallonShooter_GameManager.instance != null)
                {
                    BallonShooter_GameManager.instance.ballonPopCount();
                }
            }
        }
        else
        {
            Debug.DrawRay(origin, direction * debugLength, Color.red, 1f);
            Debug.Log("[VR] Aucune cible touchée");
        }
    }
}
