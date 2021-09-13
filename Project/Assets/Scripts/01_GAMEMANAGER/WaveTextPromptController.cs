using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WaveTextPromptController : MonoBehaviour
{
    [SerializeField] TMP_Text wavePromptNum;

    private int currentWaveNum;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        wavePromptNum.text = currentWaveNum.ToString();
        StartCoroutine(DeactivateWavePrompter());
    }

    IEnumerator DeactivateWavePrompter()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }

    public void SetWaveIndex(int waveIndex)
    {
        currentWaveNum = waveIndex;
    }
}
