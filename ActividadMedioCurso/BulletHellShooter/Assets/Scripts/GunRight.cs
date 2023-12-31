using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// <c>GunRight</c>
/// Handles the shooting from right collider
/// </summary>
public class GunRight : MonoBehaviour
{
    public Transform bulletSpawnPointRight;

    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public int bulletCountRight = 0;

    private float bulletDelay = 0.08f;

    private float startAngle = 45f, endAngle = 270f;

    /// <summary>
    /// Start coroutine of shooting
    /// </summary>
    void Start()
    {
        StartCoroutine(ShootBulletsGradually());
    }

    /// <summary>
    /// Coroutine to start the shooting of right collider
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
            float cosValue = Mathf.Cos(elapsedTime); 
            float sinValue = Mathf.Sin(elapsedTime); 
            float offsetY = cosValue * 2.5f;
            float offsetX = sinValue * 2.5f;

            if (TimeManager.Minute < 10)
            {
                ShootBulletForward();
            }
            else if (TimeManager.Minute >= 10 && TimeManager.Minute < 20)
            {
                ShootBulletFlowerWithOffset(offsetY, -1, offsetX, offsetY);
            }
            else if (TimeManager.Minute >= 20 && TimeManager.Minute < 30)
            {
                float infinityForm = (Mathf.Sin(elapsedTime) * Mathf.Cos(elapsedTime)) * 2.5f;
                offsetY = (Mathf.Cos(elapsedTime) * Mathf.Sin(elapsedTime)) * 2.5f;
                float offsetZ = Mathf.Cos(elapsedTime);
                ShootBulletFlowerWithOffset(-infinityForm, 1, -offsetY, offsetZ);
            }
            else {
                ShootBulletForward();
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
    public int GetBulletCountRight() {
        return bulletCountRight;
    }
    /// <summary>
    /// Adding to count of of bullets
    /// </summary>
    public void AddBulletCountRight() {
        bulletCountRight++;
    }
    /// <summary>
    /// Decreasing counting of of bullets
    /// </summary>
    public void DecreaseBulletCountRight() {
        bulletCountRight = Mathf.Max(0, bulletCountRight - 1);
    }

     /// <summary>
    /// Shoot a bullet forward by getting the component. Also setting the component to count bullets.
    /// </summary>
    private void ShootBulletForward()
    {
        var bulletRight = Instantiate(bulletPrefab, bulletSpawnPointRight.position, bulletSpawnPointRight.rotation);
        bulletRight.GetComponent<BulletRight>().SetGunRight(this);
        bulletRight.GetComponent<Rigidbody>().velocity = bulletSpawnPointRight.forward * bulletSpeed;
    }

    /// <summary>
    /// Shoot the bullet upwards and downards with sinoidal function
    /// </summary>
    /// <param name="offset"></param>
    private void ShootBulletFlowerWithOffset(float offsetY, int direction, float offsetX, float offsetZ)
    {
        var bulletRight = Instantiate(bulletPrefab, bulletSpawnPointRight.position, bulletSpawnPointRight.rotation);
        bulletRight.GetComponent<BulletRight>().SetGunRight(this);

        if (direction == -1)
        {
            Vector3 velocity = bulletSpawnPointRight.forward * bulletSpeed;
            velocity.y += offsetY; // Add the oscillation to the y component of the velocity

            bulletRight.GetComponent<Rigidbody>().velocity = velocity;
        }
        else 
        {
            Vector3 velocity = bulletSpawnPointRight.forward * bulletSpeed;
            velocity.x += offsetX; // Add the oscillation to the x component of the velocity
            velocity.y += offsetY;
            velocity.z += offsetZ * 1.0f;

            bulletRight.GetComponent<Rigidbody>().velocity = velocity;
        }
    }

}
