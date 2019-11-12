using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseManager : MonoBehaviour
{
    public GameObject pauseMenu;


    public void menuPause()
    {
        pauseMenu.SetActive(true);
    }

    public void menuResume()
    {
        pauseMenu.SetActive(false);
    }        


    public void mainMenu()
    {
        //Load Menu scene
        
    }

    public void menuExit()
    {
        Application.Quit();
    }

}
