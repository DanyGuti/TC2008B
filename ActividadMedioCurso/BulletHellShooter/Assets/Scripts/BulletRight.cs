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

    /// <summary>
    /// Destuir objeto de balas cada 2 segundos
    /// </summary>
    void Awake()
    {
        Destroy(gameObject, life);
    }


    /// <summary>
    /// Añadir al contador del cañón
    /// </summary>
    /// <param name="gun"></param>
    public void SetGunRight(GunRight gun)
    {
        gunRight = gun;
        gunRight.AddBulletCountRight();
    }

    /// <summary>
    /// Decrementar contador de balas del cañón
    /// </summary>
    private void OnDestroy()
    {
        if (gunRight != null)
        {
            gunRight.DecreaseBulletCountRight(); 
        }
    }
}
