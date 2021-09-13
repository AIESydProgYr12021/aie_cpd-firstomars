using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;

public class CoundownController : MonoBehaviour
{
    [SerializeField] int countdownTimerMax;
    [SerializeField] int countdown;
    [SerializeField] TMP_Text countdownText;
    [SerializeField] GameObject wavePrompt;

    private WaveTextPromptController wavePromptController;
    private float countDownTimer;

    [Serializable]
    public class OnTimerEvent : UnityEvent { }

    public OnTimerEvent onStart;
    public OnTimerEvent onTick;
    public OnTimerEvent onEnd;

    private void Awake()
    {
        wavePromptController = wavePrompt.GetComponent<WaveTextPromptController>();
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        countDownTimer = countdownTimerMax;
        countdown = countdownTimerMax;
        countdownText.text = countdown.ToString();
        
        onStart?.Invoke();
    }

    private void Update()
    {
        countDownTimer -= Time.deltaTime;
        countdownText.text = ((int)countDownTimer + 1).ToString();

        if(countDownTimer <= 0.0f)
        {
            gameObject.SetActive(false);
            onEnd?.Invoke();
        }
            
    }

    public void SetWaveIndex(int waveIndex)
    {
        wavePromptController.SetWaveIndex(waveIndex + 1);
    }
}
