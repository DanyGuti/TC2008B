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
    public void SetGunLeft(GunLeft gun)
    {
        gunLeft = gun;
        gunLeft.AddBulletCountLeft();
    }

    /// <summary>
    /// Decrementar contador de balas del cañón
    /// </summary>
    private void OnDestroy()
    {
        if (gunLeft != null)
        {
            gunLeft.DecreaseBulletCountLeft();
        }
    }
}
