using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>CoreographyUI</c>
/// Clase para ver tipo de coreografía UI
/// </summary>
public class CoreographyUI : MonoBehaviour
{
    public Text coreographyType;

    // Update is called once per frame
    void Update()
    {
        updateText();
    }

     /// <summary>
    /// Despliegue en UI de mensaje de tipo de coreografía
    /// </summary>
    public void updateText()
    {
        if (TimeManager.Minute > 0 && TimeManager.Minute < 10)
        {
            coreographyType.text = "Disparo recto";
        }
        else if (TimeManager.Minute >= 10 && TimeManager.Minute < 20)
        {
            coreographyType.text = "Disparo sinoidal eje y";
        }
        else if (TimeManager.Minute >= 20 && TimeManager.Minute < 30)
        {
            coreographyType.text = "Disparo sinoidal eje x";
        }
        else
        {
            coreographyType.text = "Disparo recto";
        }
    }
}
