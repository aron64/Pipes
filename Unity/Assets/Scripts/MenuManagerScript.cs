using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagerScript : MonoBehaviour
{
    /// <summary>
    /// Adding reference for main camera
    /// </summary>
    public Camera MainCam;

    /// <summary>
    ///Létrehozom a három menünek a kódban lévő megfelelőjét, mint "GameObject", amikkel fogok dolgozni.
    /// </summary>
    public GameObject MainMenu, OptionsMenu, HelpMenu;


    /// <summary>
    ///Létrehoztam egy publikus AudioSource ami elfogja tárolni a zenét.
    /// </summary>
    public AudioSource music;

    /// <summary>
    ///Az itt létrehozott nyomógomb azért szükésges mert ezzel fogom ellenőrizni, hogy a felhasználó szeretné-e a zenét hallgatni vagy inkább kikapcsolja.
    /// </summary>
    public Toggle toggle;

    /// <summary>
    /// Set to true for TDD
    /// </summary>
    public static bool test = false;

    
    ///Inicalizáció:
    ///Minden más menüt lekapcsolok és a főmenüt meg bekapcsolom
    void Start() {
        //if(MainMenu.activeSelf != true)
        MainMenu.SetActive(true);
        OptionsMenu.SetActive(false);
        HelpMenu.SetActive(false);

        if (test) {  StartCoroutine(TDD()); }
    }

    /// <summary>
    /// TDD - a pályát látjuk 1s-ig, majd vissza a menübe
    /// </summary>
    IEnumerator TDD()
    {
        MainMenu.SetActive(false);
        yield return new WaitForSeconds(1);
        MainMenu.SetActive(true);
        
    }
    public void ToggleChanged()
    {
        if(toggle.isOn == true)
        {
            music.Play();
        }
        else
        {
            music.Stop();
        }
    }

    private void Update() {

    }

    

    /// <summary>
    /// Gombok kezelése:
    /// Nyissa meg a főmenüt
    /// </summary>
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Zárja be a főmenüt
    /// </summary>
    public void CloseMainMenu()
    {
        MainMenu.SetActive(false);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Nyissa meg a beállítások menüt
    /// </summary>
    public void OpenOptionsMenu()
    {
        OptionsMenu.SetActive(true);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Zárja be a beállítások menüt
    /// </summary>
    public void CloseOptionsMenu()
    {
        OptionsMenu.SetActive(false);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Nyissa meg a segítség menüt
    /// </summary>
    public void OpenHelpMenu()
    {
        HelpMenu.SetActive(true);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Zárja be a segítség menüt
    /// </summary>
    public void CloseHelpMenu()
    {
        HelpMenu.SetActive(false);
    }
}
