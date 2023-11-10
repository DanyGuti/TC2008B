using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    public void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    /// <summary>
    /// Validación del tiempo a las 10:30
    /// </summary>
    private void TimeCheck()
    {
        // if(TimeManager.Hour == 10 && TimeManager.Minute == 30)
        // {
        //     StartCoroutine(MoveSquare());
        // }
        if (TimeManager.Minute % 10 == 0) {
            StartCoroutine(MoveSquare());
        }
    }

    /// <summary>
    /// Modifica la pocisión de vector inicial y final de los Vector3
    ///  para que se vayan alineando a tu propia interfaz.
    /// </summary>
    /// <returns></returns>
    private IEnumerator MoveSquare()
    {
        transform.position = new Vector3(-291f, 503f, 0);
        Vector3 targetPos = new Vector3(1828f, 503f, 0);

        Vector3 currentPos = transform.position;

        float timeElapsed = 0;
        float timeToMove = 2;

        while(timeElapsed < timeToMove) {
            transform.position = Vector3.Lerp(currentPos, targetPos, timeElapsed/timeToMove);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

}
