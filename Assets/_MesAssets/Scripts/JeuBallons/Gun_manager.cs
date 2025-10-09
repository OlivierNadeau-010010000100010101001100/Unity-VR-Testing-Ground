using UnityEngine;
using UnityEngine.InputSystem; // Nécessaire si tu utilises le nouveau Input System (recommandé en VR)

public class VRGunManager : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private Transform gunPoint;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;
    
    private string targetTag = "Ballon";


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
                if (BallonShooter_GameManager.instance != null)
                {
                    BallonShooter_GameManager.instance.ballonPopCount();
                    hit.collider.GetComponent<BallonPop>().Sound();
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
