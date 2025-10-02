using UnityEngine;
using UnityEngine.InputSystem; // Nécessaire si tu utilises le nouveau Input System (recommandé en VR)

public class VRGunManager : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private Transform gunPoint;         // Transform du canon ou main
    [SerializeField] private string targetTag = "Enemy"; // Tag à détecter

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootClip;

    //[Header("Input")]
    //[SerializeField] private InputActionReference shootAction; // Action input de la gâchette

    //private void OnEnable()
    //{
    //    shootAction.action.performed += OnShootPerformed;
    //    shootAction.action.Enable();
    //}

    //private void OnDisable()
    //{
    //    shootAction.action.performed -= OnShootPerformed;
    //    shootAction.action.Disable();
    //}

    //private void OnShootPerformed(InputAction.CallbackContext context)
    //{
    //    Shoot();
    //}

    public void Shoot()
    {
        if (audioSource && shootClip)
        {
            audioSource.PlayOneShot(shootClip);
        }

        Vector3 origin = gunPoint.position;
        Vector3 direction = gunPoint.forward;
        float debugLength = 100f;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, Mathf.Infinity))
        {

            if (hit.collider.CompareTag(targetTag))
            {
                Debug.DrawRay(origin, direction * debugLength, Color.green, 1f);
                Debug.Log($"[VR] Cible touchée : {hit.collider.name}");
            }
        }
        else
        {
            Debug.DrawRay(origin, direction * debugLength, Color.red, 1f);
            Debug.Log("[VR] Aucune cible touchée");
        }
    }
}
