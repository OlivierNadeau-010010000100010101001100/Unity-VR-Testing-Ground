using System;
using UnityEngine;

public class BallonShooter_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gun;

    private GameObject[] _balloonsTable;

    private Vector3 _positionStartGun;
    private Quaternion _rotationStartGun;

    private float _timeStart;
    private float _timeEnd;
    private bool _timerRunning = false;

    private int _numberBalloonTotal;
    private bool _gunTaken = false;

    public float TempsDepart => _timeStart;
    public float TempsFin => _timeEnd;
    public bool TimerActif => _timerRunning;

    public event Action<float> EventUpdateTime;

    public static BallonShooter_GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _positionStartGun = _gun.transform.localPosition;
        _rotationStartGun = _gun.transform.localRotation;

        // Récupère tous les ballons avec le tag "Ballon"
        _balloonsTable = GameObject.FindGameObjectsWithTag("Ballon");
        _numberBalloonTotal = _balloonsTable.Length;

        // Mettre le meilleur temps au démarrage
        if (PlayerPrefs.HasKey("BestTime"))
        {
            float bestTime = PlayerPrefs.GetFloat("BestTime");
            Debug.Log($"Meilleur temps actuel : {bestTime:F3}s");
            EventUpdateTime?.Invoke(bestTime);
        }
        else
        {
            Debug.Log("Aucun meilleur temps enregistré.");
        }
    }

    public void StartTimer()
    {
        if (!_gunTaken)
        {
            _gunTaken = true;
            _timeStart = Time.time;
            _timerRunning = true;
        }
    }

    public void EndTimer()
    {
        if (_timerRunning)
        {
            _timerRunning = false;
            float tempsFinal = Time.time - _timeStart;
            _timeEnd = tempsFinal;

            if (tempsFinal <= 0f)
            {
                Debug.Log("Temps final nul, enregistrement ignoré.");
                return;
            }

            float meilleurTemps = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
            if (!PlayerPrefs.HasKey("BestTime") || tempsFinal < meilleurTemps)
            {
                PlayerPrefs.SetFloat("BestTime", tempsFinal);
                PlayerPrefs.Save();
                EventUpdateTime?.Invoke(tempsFinal);
                Debug.Log($"Nouveau meilleur temps enregistré : {tempsFinal:F3}s");
            }
        }
    }

    public void ballonPopCount()
    {
        _numberBalloonTotal--;
        Debug.Log($"Ballons restants : {_numberBalloonTotal}");

        if (_numberBalloonTotal <= 0)
        {
            EndTimer();
        }
    }

    [ContextMenu("ResetGame")]
    public void ResetGame()
    {
        _gun.transform.localPosition = _positionStartGun;
        _gun.transform.localRotation = _rotationStartGun;
        _gunTaken = false;
        _timeStart = 0f;
        _timerRunning = false;

        foreach (var balloon in _balloonsTable)
        {
            if (balloon != null)
                balloon.SetActive(true);
        }

        _numberBalloonTotal = _balloonsTable.Length;

        Debug.Log("Partie réinitialisée");
    }

    [ContextMenu("Réinitialiser le Meilleur Temps")]
    private void ResetBestTime()
    {
        PlayerPrefs.DeleteKey("BestTime");
        PlayerPrefs.Save();
        Debug.Log("Meilleur temps réinitialisé.");
    }
}