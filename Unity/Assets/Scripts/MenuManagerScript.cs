using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagerScript : MonoBehaviour
{
    ///Létrehozom a három menünek a kódban lévő megfelelőjét, mint "GameObject", amikkel fogok dolgozni.
    public GameObject MainMenu,OptionsMenu,HelpMenu;

    ///Létrehoztam egy publikus AudioSource ami elfogja tárolni a zenét.
    public AudioSource music;

    ///Az itt létrehozott nyomógomb azért szükésges mert ezzel fogom ellenőrizni, hogy a felhasználó szeretné-e a zenét hallgatni vagy inkább kikapcsolja.
    public Toggle toggle;
    
    ///Ellenőrzöm, hogy az indulásnál a főmenu van-e bekapcsolva, ha nem akkor minden mást lekapcsolok és a főmenüt meg bekapcsolom
    private void Start() {
        if(MainMenu.active != true)
        {
            MainMenu.SetActive(true);
            OptionsMenu.SetActive(false);
            HelpMenu.SetActive(false);
        }
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
        //if(toggle.isOn == true)
        //{
        //    music.Play();
        //}
        //else
        //{
        //    music.Stop();
        //}
    }

    ///A függvények a gombok kattintására végrehajtják a bennük leírtaka. 
    ///--Nyissa meg a főmenüt, zárja be a főmenüt,nyissa meg a beállítások menüt, zárja be a beállítások menüt, nyissa meg a segítség menüt, zárja be a segítség menüt.--
    public void OpenMainMenu()
    {
        MainMenu.SetActive(true);
    }

    public void CloseMainMenu()
    {
        MainMenu.SetActive(false);
    }

    public void OpenOptionsMenu()
    {
        OptionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        OptionsMenu.SetActive(false);
    }

    public void OpenHelpMenu()
    {
        HelpMenu.SetActive(true);
    }

    public void CloseHelpMenu()
    {
        HelpMenu.SetActive(false);
    }
}
