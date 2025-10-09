using TMPro;
using UnityEngine;
using System;

public class UI_manager : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtTime;
    [SerializeField] private TMP_Text _txtBestTime;


    private void Start()
    {
        BallonShooter_GameManager.instance.EventUpdateTime += OnEventBestTime;
    }
    private void OnDestroy()
    {
        BallonShooter_GameManager.instance.EventUpdateTime -= OnEventBestTime;
    }


    private void Update()
    {
        TimeSpent();
    }

    private void TimeSpent()
    {
        float time = Time.time - BallonShooter_GameManager.instance.TempsDepart;
        _txtTime.text = "Votre Temps : " + TimeSpan.FromSeconds(time).ToString("mm\:ss\.fff");

    }

    private void OnEventBestTime(float tmps)
    {
        _txtBestTime.text = "Meilleur Temps : " + TimeSpan.FromSeconds(tmps).ToString("mm\:ss\.fff");
    }
}
