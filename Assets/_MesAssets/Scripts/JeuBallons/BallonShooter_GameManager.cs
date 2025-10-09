using System;
using UnityEngine;

public class BallonShooter_GameManager : MonoBehaviour
{
    private GameObject[] _balloonsTable;
    [SerializeField] private GameObject _gun;

    private Vector3 _positionStartGun;
    private Quaternion _rotationStartGun;

    public static BallonShooter_GameManager instance;

    private float _timeStart;
    private bool _timerRunning = false;
    public float TempsDepart => _timeStart;
    public bool TimerActif => _timerRunning;

    private int _numberBalloonTotal;
    private bool _gunTaken = false;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        _positionStartGun = _gun.transform.localPosition;
        _rotationStartGun = _gun.transform.localRotation;

        _balloonsTable = GameObject.FindGameObjectsWithTag("Ballons");
        _numberBalloonTotal = _balloonsTable.Length;

        // mettre le meilleur temps au démarrage
        EventUpdateTime?.Invoke(PlayerPrefs.GetFloat("BestTime"));
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

            // Vérifie et sauvegarde le meilleur temps
            float meilleurTemps = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
            if (tempsFinal < meilleurTemps)
            {
                PlayerPrefs.SetFloat("BestTime", tempsFinal);
                PlayerPrefs.Save();
                EventUpdateTime?.Invoke(tempsFinal);
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
            balloon.SetActive(true);
        }
        _numberBalloonTotal = _balloonsTable.Length;

        Debug.Log("Partie réinitialisée");
    }

    public event Action<float> EventUpdateTime;
}
