using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <c>BulletRight</c>
/// Agregar uno a las balas y quitar cuando se necesite
/// </summary>
public class BulletRight : MonoBehaviour
{
    public float life = 2;
    public GunRight gunRight;

    void Awake()
    {
        Destroy(gameObject, life);
    }

    public void SetGunRight(GunRight gun)
    {
        gunRight = gun;
        gunRight.AddBulletCountRight(); // Increment bullet count in GunRight script
    }

    private void OnDestroy()
    {
        if (gunRight != null)
        {
            gunRight.DecreaseBulletCountRight(); // Decrement bullet count in GunRight script when the bullet is destroyed
        }
    }
}
