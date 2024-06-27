using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DoubleCoinsTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private bool isTimer = true;
    private float currentTime;

    public float bonusTime = 5.0f;

    private void Start()
    {
        currentTime = bonusTime;
    }

    void Update()
    {
        DoTimer();
    }

    private void DoTimer()
    {
        if (isTimer)
        {
            timerText.text = ((int)currentTime).ToString();
            currentTime -= Time.deltaTime;
        }

        if (currentTime <= 0.0f)
        {
            isTimer = false;
            this.gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        currentTime = bonusTime;
        isTimer = true;
    }
}
