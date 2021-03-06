﻿using TMPro;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;

    private float timer;
    private bool triggered;

    private void Update()
    {
        if (!triggered)
        {
            timer += Time.deltaTime;
            timerText.text = "" + (int)timer;
        }
    }

    public float GetTime()
    {
        return timer;
    }

    public void SetWinState()
    {
        triggered = true;
        MyUtils.PauseGame(true);
    }
}