using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    public void OnEnable()
    {
        TimeManager.OnMinuteChanged += TimeCheck;
    }

    public void OnDisable()
    {
        TimeManager.OnMinuteChanged -= TimeCheck;
    }

    private void TimeCheck()
    {
        if (TimeManager.Minute % 20 == 0){
            StartCoroutine(MoveTriangle());
        }
    }

    private IEnumerator MoveTriangle()
    {
        transform.position = new Vector3(-280f, 503f, 0);
        Vector3 targetPos = new Vector3(750f, 119f, 0);
        Vector3 finalPos = new Vector3(1828f, 503f, 0);

        Vector3 currentPos = transform.position;

        transform.localScale = new Vector3(100f, 100f, 100f);
        Vector3 targetScale = new Vector3(250f, 250f, 250f);

        Vector3 initialScale = transform.localScale;

        float timeElapsed = 0;
        float timeToMove = 4;

        while(timeElapsed < timeToMove) {
            float t = (timeElapsed / (timeToMove / 2));
            float rotationAngle = 360f * t;
            transform.position = Vector3.Lerp(currentPos, targetPos, t);
            transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
            transform.rotation = Quaternion.Euler(0, 0, rotationAngle);
            if(Vector3.Distance(transform.localScale, targetScale) < 1.0f)
            {
                transform.rotation = Quaternion.Euler(0, 0, (360f * t * 2f));
            }
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.localScale = targetScale;
        transform.rotation = Quaternion.Euler(0, 0, 360f);
    }
}
