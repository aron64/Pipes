using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Beállítások menü:
/// Hang és Pályaméret beállítása
/// </summary>
public class OptionsMenu : MonoBehaviour
{
    /// <summary>
    /// Referencia a csúszkához
    /// </summary>
    public UnityEngine.UI.Slider MapSlider;
    /// <summary>
    /// Referencia a Pályaméret megjelenítéséhez
    /// </summary>
    public UnityEngine.UI.Text SizeText;

    // Start is called before the first frame update
    void Start()
    {
        MapSlider.minValue = 8;
        MapSlider.maxValue = 20;
        MapSlider.value = 8;
        MapSlider.wholeNumbers = true;
    }
    
    /// <summary>
    /// A pálya méretének frissítése globálisan
    /// </summary>
    public void RefreshValue()
    {
        SizeText.text = MapSlider.value.ToString();
        SizeText.text += "x" + SizeText.text;
        Map.MapSize = (int)MapSlider.value;
    }
}
