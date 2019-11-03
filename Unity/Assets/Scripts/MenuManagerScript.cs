using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Menu vezérlő
/// </summary>
public class MenuManagerScript : MonoBehaviour
{
    /// <summary>
    /// Adding reference for main camera
    /// </summary>
    public Camera MainCam;

    /// <summary>
    ///Létrehozom a három menünek a kódban lévő megfelelőjét, mint "GameObject", amikkel fogok dolgozni.
    /// </summary>
    public GameObject mainMenu, optionsMenu, helpMenu,gameModes, exitMenuGm1, exitMenuGm2;


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

    /// <summary>
    /// Játék megállítása
    /// </summary>
    public bool pauseActivated = false;

    /// <summary>
    /// Jelenleg futó játék
    /// </summary>
    public static int activeMode = 0;

    /// <summary>
    /// Float a hanerő szabályozóhoz
    /// </summary>
    private float musicVolume = 0.1f;

    ///Inicalizáció:
    ///Minden más menüt lekapcsolok és a főmenüt meg bekapcsolom
    void Start() {
        //if(MainMenu.activeSelf != true)
        music.volume = musicVolume;
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
        helpMenu.SetActive(false);
        gameModes.SetActive(false);
        exitMenuGm1.SetActive(false);
        exitMenuGm2.SetActive(false);

        if (test) {  StartCoroutine(TDD()); }
    }

    /// <summary>
    /// Szünet capture
    /// </summary>
    private void Update()
    {
        music.volume = musicVolume;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (activeMode != 0)
            {
                GameObject active= new GameObject();
                switch (activeMode)
                {
                    case 1:
                        active = exitMenuGm1;
                        break;
                    case 2:
                        active = exitMenuGm2;
                        break;
                    default:
                        break;
                }
                pauseActivated = active.activeSelf;
                if (!pauseActivated)
                {
                    active.SetActive(true);
                }
                else
                {
                    active.SetActive(false);
                }
            }
        }
    }
    /// <summary>
    /// TDD - a pályát látjuk 1s-ig, majd vissza a menübe
    /// </summary>
    IEnumerator TDD()
    {
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(1);
        mainMenu.SetActive(true);
        
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

    /// <summary>
    /// Zene hangerő beállítása
    /// </summary>
    /// <param name="vol">float 0 és 1 között</param>
    public void SetVolume(float vol)
    {
        musicVolume = vol;
    }


    /// <summary>
    /// Gombok kezelése:
    /// Nyissa meg a JátékMódok panelt
    /// </summary>
    public void OpenGameModes()
    {
        if (gameModes.activeSelf == true)
        {
            gameModes.SetActive(false);
        }
        else
        {
            gameModes.SetActive(true);
        }
        
    }

    ///<summary>
    /// Gombok kezelése:
    /// Zárja be a JátékMódok panelt
    /// </summary>
    public void CloseGameModes()
    {
        gameModes.SetActive(false);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Nyissa meg a főmenüt
    /// </summary>
    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Zárja be a főmenüt
    /// </summary>
    public void CloseMainMenu()
    {
        mainMenu.SetActive(false);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Nyissa meg a beállítások menüt
    /// </summary>
    public void OpenOptionsMenu()
    {
        optionsMenu.SetActive(true);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Zárja be a beállítások menüt
    /// </summary>
    public void CloseOptionsMenu()
    {
        optionsMenu.SetActive(false);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Nyissa meg a segítség menüt
    /// </summary>
    public void OpenHelpMenu()
    {
        helpMenu.SetActive(true);
    }

    /// <summary>
    /// Gombok kezelése:
    /// Zárja be a segítség menüt
    /// </summary>
    public void CloseHelpMenu()
    {
        helpMenu.SetActive(false);
    }

    /// <summary>
    /// Gombok kezelése: Lépjen vissza a játékból a főmenübe
    /// </summary>
    public void CloseGameModeOne()
    {
        mainMenu.SetActive(true);
        exitMenuGm1.SetActive(false);
    }

    /// <summary>
    /// Gombok kezelése: Lépjen vissza a játékból a főmenübe
    /// </summary>
    public void BackToPlayGM1()
    {
        exitMenuGm1.SetActive(false);
    }

    /// <summary>
    /// Gombok kezelése: Lépjen vissza a játékból a főmenübe
    /// </summary>
    public void CloseGameModeTwo()
    {
        mainMenu.SetActive(true);
        exitMenuGm2.SetActive(false);
    }

    /// <summary>
    /// Gombok kezelése: Lépjen vissza a játékból a főmenübe
    /// </summary>
    public void BackToPlayGM2()
    {
        exitMenuGm2.SetActive(false);
    }
}
