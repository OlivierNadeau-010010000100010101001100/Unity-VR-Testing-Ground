using TMPro;
using UnityEngine;
using System;

public class UI_manager : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtTime;
    [SerializeField] private TMP_Text _txtBestTime;

    private void Start()
    {
        if (BallonShooter_GameManager.instance != null)
        {
            BallonShooter_GameManager.instance.EventUpdateTime += OnEventBestTime;

            float bestTime = PlayerPrefs.GetFloat("BestTime");
            _txtBestTime.text = "Meilleur Temps : " + TimeSpan.FromSeconds(bestTime).ToString(@"mm\:ss\.ff");
            Debug.Log($"Meilleur temps actuel : {bestTime:F3}s");
        }

    }

    private void OnDestroy()
    {
        if (BallonShooter_GameManager.instance != null)
        {
            BallonShooter_GameManager.instance.EventUpdateTime -= OnEventBestTime;
        }
    }

    private void Update()
    {
        if (BallonShooter_GameManager.instance != null && BallonShooter_GameManager.instance.TimerActif)
        {
            TimeSpent();
        }
    }

    private void TimeSpent()
    {
        float time = Time.time - BallonShooter_GameManager.instance.TempsDepart;
        TimeSpan timeSpan = TimeSpan.FromSeconds(time+0.01f);
        _txtTime.text = "Votre Temps : " + timeSpan.ToString(@"mm\:ss\.ff");
    }

    private void OnEventBestTime(float tmps)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(tmps);
        _txtBestTime.text = "Meilleur Temps : " + timeSpan.ToString(@"mm\:ss\.ff");
    }
}