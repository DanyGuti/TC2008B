using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Class <c>TimeManager</c> Modela cantidad de tiempo con conversión
/// </summary>
public class TimeManager : MonoBehaviour
{
    // Para cambiar el estado del juego lanzando eventos
    public static Action OnMinuteChanged;
    // Para cambia el estado del juego lanzando eventos
    public static Action OnHourChanged;

    // cualquier elemento externo puede acceder a los valores,
    // pero solo dentro del TimeManager se pueden modificar
    public static int Minute{get; private set;}
    public static int Hour{get;private set;}

    private float minuteToRealTime = 0.9f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        Minute = 0;
        Hour = 0;
        timer = minuteToRealTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Minute++;

            OnMinuteChanged?.Invoke();

            if(Minute >= 60)
            {
                Hour++;
                OnHourChanged?.Invoke();
                Minute = 0;
            }

            timer = minuteToRealTime;
        }
    }
}
