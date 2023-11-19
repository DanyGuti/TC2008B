using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// <c>GunLeft</c>
/// Handles the shooting from left collider
/// </summary>
public class GunLeft : MonoBehaviour
{
    public Transform bulletSpawnPointLeft;

    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public int bulletCountLeft = 0;

    private float bulletDelay = 0.08f;

    private float startAngle = 45f, endAngle = 270f;

    private bool appliedCameraChangeXshoot = false;
    private bool appliedCameraChangeYshoot = false;
    /// <summary>
    /// Start coroutine of shooting
    /// </summary>
    void Start()
    {
        StartCoroutine(ShootBulletsGradually());
    }

    /// <summary>
    /// Coroutine to start the shooting of left collider
    /// </summary>
    /// <returns>
    /// Delay in between bullets
    /// </returns>
    private IEnumerator ShootBulletsGradually()
    {
        float elapsedTime = 0f;
        Camera mainCamera = Camera.main;
        while(TimeManager.Hour < 10)
        {
            float sinValue = Mathf.Sin(elapsedTime); 
            float offset = sinValue * 2.5f;

            if (TimeManager.Minute < 10)
            {
                ShootBulletForward();
            }
            else if (TimeManager.Minute >= 10 && TimeManager.Minute < 20)
            {
                if (!appliedCameraChangeYshoot)
                {
                    // Transition camera view before shooting flower bullets
                    StartCoroutine(TransitionCameraView(mainCamera.transform.position, mainCamera.transform.rotation, new Vector3(-2.45f, 5.05f, -13.82f), Quaternion.Euler(7.668f, 17.332f, -3.615f), 2f));
                    // Wait for the camera transition
                    appliedCameraChangeYshoot = true;
                }
                ShootBulletFlowerWithOffset(offset, -1);
            }
            else if (TimeManager.Minute >= 20 && TimeManager.Minute < 30)
            {
                if (!appliedCameraChangeXshoot)
                {
                    // Transition camera view before shooting flower bullets
                    StartCoroutine(TransitionCameraView(mainCamera.transform.position, mainCamera.transform.rotation, new Vector3(-2.45f, 5.05f, -13.82f), Quaternion.Euler(7.668f, 17.332f, -3.615f), 2f));
                    // Wait for the camera transition
                    appliedCameraChangeXshoot = true;
                }
                ShootBulletFlowerWithOffset(offset, 1);
            }
            else
            {
                ShootBulletForward();
                appliedCameraChangeXshoot = false;
                appliedCameraChangeYshoot = false;
            }
            elapsedTime += bulletDelay;
            yield return new WaitForSeconds(bulletDelay);
        }
    }

    /// <summary>
    /// Get the amount of bullets;
    /// </summary>
    /// <returns>
    /// Number of bullets
    /// </returns>
    public int GetBulletCountLeft() {
        return bulletCountLeft;
    }

     /// <summary>
    /// Adding to count of of bullets
    /// </summary>
    public void AddBulletCountLeft() {
        bulletCountLeft++;
    }

    /// <summary>
    /// Decreasing counting of of bullets
    /// </summary>
    public void DecreaseBulletCountLeft() {
        bulletCountLeft = Mathf.Max(0, bulletCountLeft - 1);
    }

    /// <summary>
    /// Shoot a bullet forward by getting the component. Also setting the component to count bullets.
    /// </summary>
    private void ShootBulletForward()
    {
        var bulletLeft = Instantiate(bulletPrefab, bulletSpawnPointLeft.position, bulletSpawnPointLeft.rotation);
        bulletLeft.GetComponent<BulletLeft>().SetGunLeft(this);
        bulletLeft.GetComponent<Rigidbody>().velocity = bulletSpawnPointLeft.forward * bulletSpeed;
    }

    /// <summary>
    /// Shoot the bullet upwards and downards with sinoidal function
    /// </summary>
    /// <param name="offset"></param>
    private void ShootBulletFlowerWithOffset(float offset, int direction)
    {
        var bulletLeft = Instantiate(bulletPrefab, bulletSpawnPointLeft.position, bulletSpawnPointLeft.rotation);
        bulletLeft.GetComponent<BulletLeft>().SetGunLeft(this);

        if (direction == -1)
        {
            Vector3 velocity = bulletSpawnPointLeft.forward * bulletSpeed;
            velocity.y += offset; // Add the oscillation to the y component of the velocity

            bulletLeft.GetComponent<Rigidbody>().velocity = velocity;
        }
        else 
        {
            Vector3 velocity = bulletSpawnPointLeft.forward * bulletSpeed;
            velocity.x += offset; // Add the oscillation to the x component of the velocity

            bulletLeft.GetComponent<Rigidbody>().velocity = velocity;
        }
    }

    /// <summary>
    /// Move the camera of position and rotation
    /// </summary>
    /// <param name="startCamPosition"></param>
    /// <param name="startCamRotation"></param>
    /// <param name="endCamPosition"></param>
    /// <param name="endCamRotation"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    private IEnumerator TransitionCameraView(Vector3 startCamPosition, Quaternion startCamRotation, Vector3 endCamPosition, Quaternion endCamRotation, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            Camera.main.transform.position = Vector3.Lerp(startCamPosition, endCamPosition, timeElapsed / duration);
            Camera.main.transform.rotation = Quaternion.Lerp(startCamRotation, endCamRotation, timeElapsed / duration);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the final camera position and rotation are set exactly to the target values
        Camera.main.transform.position = endCamPosition;
        Camera.main.transform.rotation = endCamRotation;
    }

}
