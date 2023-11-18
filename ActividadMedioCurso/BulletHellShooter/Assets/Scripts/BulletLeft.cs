using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>BulletLeft</c>
/// Agregar uno a las balas y quitar cuando se necesite
/// </summary>
public class BulletLeft : MonoBehaviour
{
    public float life = 2;
    public GunLeft gunLeft;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    public void SetGunLeft(GunLeft gun)
    {
        gunLeft = gun;
        gunLeft.AddBulletCountLeft(); // Increment bullet count in GunRight script
    }

    private void OnDestroy()
    {
        if (gunLeft != null)
        {
            gunLeft.DecreaseBulletCountLeft(); // Decrement bullet count in GunRight script when the bullet is destroyed
        }
    }
}
