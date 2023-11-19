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

    /// <summary>
    /// Actualizar el tiempo
    /// </summary>
    private void OnEnable()
    {
        TimeManager.OnMinuteChanged += UpdateTime;
        TimeManager.OnHourChanged += UpdateTime;
    }

    /// <summary>
    /// Decrementar tiempo cuando hay inactividad
    /// </summary>
    private void OnDisable()
    {
        TimeManager.OnMinuteChanged -= UpdateTime;
        TimeManager.OnHourChanged -= UpdateTime;
    }

    /// <summary>
    /// Actualizar tiempo en UI
    /// </summary>
    private void UpdateTime()
    {
        timeText.text = $"{TimeManager.Hour.ToString("00")}:{TimeManager.Minute:00}";
    }
}
