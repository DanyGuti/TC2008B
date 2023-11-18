using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>CountBulletUI</c>
/// Contar balas y despliegue en UI
/// </summary>
public class CountBulletsUI : MonoBehaviour
{
    public Text ContadorBalas;
    public GunRight gunRight;
    public GunLeft gunLeft;

    public void Update()
    {
        updateText();
    }
    
    /// <summary>
    /// Despliegue en UI de mensaje de contador de balas
    /// </summary>
    public void updateText()
    {
        gameObject.SetActive(true);
        int countBullets = gunRight.GetBulletCountRight() + gunLeft.GetBulletCountLeft();

        ContadorBalas.text = "Contador de balas: "+ countBullets.ToString();
    }
}
