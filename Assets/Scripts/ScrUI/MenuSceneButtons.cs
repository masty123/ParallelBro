using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuSceneButtons : MonoBehaviour
{

    public GameObject loginButton;

    public GameObject menuCanvas;

    public bool isSuccess;

    private void Start()
    {

    }

    public void Login()
    {
        menuCanvas.SetActive(true);
        Debug.Log("Open menu");
        loginButton.SetActive(false);

    }

    public void Play()
    {

    }

    public void Option()
    {

    }

    public void Exit()
    {

    }



}
