using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    [SerializeField] float startTime;

    private void Update()
    {
        float t = startTime - Time.time;
        if (t >= 0)
        {
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            timerText.text = minutes + ":" + seconds;
        }
        else
        {
            // Application.Quit();
            Debug.Log("Game Over");
        }
    }
}
