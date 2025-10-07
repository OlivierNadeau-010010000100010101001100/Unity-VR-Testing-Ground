using UnityEngine;

public class BallonShooter_GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _balloonsTable;
    private Vector3[] _positionBalloonsStartTable;
    private Quaternion[] _rotationBalloonsStartTable;


    [SerializeField] private GameObject _gun;
    private Vector3 _positionStartGun;
    private Quaternion _rotationStartGun;

    public static BallonShooter_GameManager instance;

    private float _time;
    private int _numberBalloonTotal = 17;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _positionStartGun = _gun.transform.localPosition;
        _rotationStartGun = _gun.transform.localRotation;


        _positionBalloonsStartTable = new Vector3[_balloonsTable.Length];
        _rotationBalloonsStartTable = new Quaternion[_balloonsTable.Length];

        int i = 0;
        foreach (var balloon in _balloonsTable)
        {
            _positionBalloonsStartTable[i] = _balloonsTable[i].transform.localPosition; // LOCAL
            _rotationBalloonsStartTable[i] = _balloonsTable[i].transform.localRotation; // LOCAL
        }

    }

    public void StartTimer()
    {
        if (_time == 0f)
        {
            _time = Time.time;
        }
    }


    public void ballonPopCount()
    {

        _numberBalloonTotal--;
        Debug.Log(_numberBalloonTotal);

        if (_numberBalloonTotal <= 0)
        {

        }
    }

    public void EndTimer()
    {

    }

    [ContextMenu("ResetGame")]
    public void ResetGame()
    {
        _numberBalloonTotal = 17;

        _gun.transform.localPosition = _positionStartGun;
        _gun.transform.localRotation = _rotationStartGun;

        int i = 0;
        foreach (var balloon in _balloonsTable)
        {
            _balloonsTable[i].transform.SetLocalPositionAndRotation(
                _positionBalloonsStartTable[i],
                _rotationBalloonsStartTable[i]
            );
        }
    }
}
