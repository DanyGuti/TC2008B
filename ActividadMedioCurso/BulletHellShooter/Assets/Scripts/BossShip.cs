using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>BossShip</c>
/// Clase para dar rotación y movimiento a la nave
/// </summary>
public class BossShip : MonoBehaviour
{
    public GameObject bossShip;
    private float amp = 4.5f;
    private float freq = 3f;
    Vector3 initialPosition;
    Quaternion initialRotation;

    private float rotationSpeed = 360f; // Adjust the initial rotation speed
    void Start()
    {
        initialPosition = bossShip.transform.position;
        initialRotation = bossShip.transform.rotation;
        StartCoroutine(RotateObject());
    }

    IEnumerator RotateObject()
    {
        float elapsedTime = 0f;
        float rotationDuration = 7f;

        while (elapsedTime < rotationDuration)
        {
            bossShip.transform.Rotate(0, 0, 360 * Time.deltaTime / rotationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the object ends exactly at 360 degrees
        bossShip.transform.rotation = Quaternion.Euler(0, 360, 0);
    }

    void Update()
    {
        if (TimeManager.Minute > 10 && TimeManager.Minute <= 30)
        {
            Vector3 newPosition = bossShip.transform.position;
            newPosition.y = Mathf.Sin(Time.time * freq) * amp ;
            bossShip.transform.position = newPosition;
            // Slow down the rotation speed over time
            rotationSpeed = Mathf.Lerp(rotationSpeed, 0f, 0.3f * Time.deltaTime);

            // Rotate the object around the y-axis by the current rotation speed
            bossShip.transform.Rotate(0, 0 , rotationSpeed * Time.deltaTime);
        }
        if (TimeManager.Minute >= 30)
        {
            // Lerp the position towards the initial position gradually
            bossShip.transform.position = Vector3.Lerp(bossShip.transform.position, initialPosition, 0.4f * Time.deltaTime);

            // Lerp the rotation towards the initial rotation gradually
            bossShip.transform.rotation = Quaternion.Lerp(bossShip.transform.rotation, initialRotation, 0.4f * Time.deltaTime);
        }
    }
}
