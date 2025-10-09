using System;
using UnityEngine;

public class BallonShooter_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gunPrefab;
    private GameObject _gunInstance;

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
        GameObject startingPoint = GameObject.FindGameObjectWithTag("StartingPoint");
        if (startingPoint == null)
        {
            return;
        }

        _gunInstance = Instantiate(_gunPrefab, startingPoint.transform.position, startingPoint.transform.rotation);
        _positionStartGun = startingPoint.transform.position;
        _rotationStartGun = startingPoint.transform.rotation;

        _balloonsTable = GameObject.FindGameObjectsWithTag("Ballon");
        _numberBalloonTotal = _balloonsTable.Length;

        if (PlayerPrefs.HasKey("BestTime"))
        {
            float bestTime = PlayerPrefs.GetFloat("BestTime");
            EventUpdateTime?.Invoke(bestTime);
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
                return;
            }

            float meilleurTemps = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
            if (!PlayerPrefs.HasKey("BestTime") || tempsFinal < meilleurTemps)
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

        if (_numberBalloonTotal <= 0)
        {
            EndTimer();
        }
    }

    [ContextMenu("ResetGame")]
    public void ResetGame()
    {
        if (_gunInstance != null)
            Destroy(_gunInstance);

        GameObject startingPoint = GameObject.FindGameObjectWithTag("StartingPoint");
        if (startingPoint == null)
        {
            return;
        }

        _gunInstance = Instantiate(_gunPrefab, startingPoint.transform.position, startingPoint.transform.rotation);
        _gunTaken = false;
        _timeStart = 0f;
        _timerRunning = false;

        foreach (var balloon in _balloonsTable)
        {
            if (balloon != null)
                balloon.SetActive(true);
        }

        _numberBalloonTotal = _balloonsTable.Length;
    }

    [ContextMenu("Réinitialiser le Meilleur Temps")]
    private void ResetBestTime()
    {
        PlayerPrefs.DeleteKey("BestTime");
        PlayerPrefs.Save();
    }
}