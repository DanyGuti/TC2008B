using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>TimeUI</c>
/// Clase para medir tiempo en base al manager
/// </summary>
public class TimeUI : MonoBehaviour
{
    public Text timeText;

    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour.ToString("00")}:{TimeManager.Minute:00}";
    }
}
