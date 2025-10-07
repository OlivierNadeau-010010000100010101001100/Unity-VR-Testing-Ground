using TMPro;
using UnityEngine;

public class UI_manager : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtTime;

    private void OnEventUpdatePointage(Time time)
    {
        _txtTime.text = "Votre Temps : " + time.ToString();
    }

    //private void OnEventBestTime()
    //{

    //}
}
