using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Scenemanager : MonoBehaviour
{

    public TMP_Text text_Timer;
    public GameObject ClearScreen;
    public static bool isWin = false;

    private float timer = 0.0f;

    void Start()
    {
        if (ClearScreen)
        {
            timer = Time.realtimeSinceStartup;
            DisplayTime();
        }
    }

    private void Update()
    {
        if (isWin && ClearScreen) ClearScreen.SetActive(true);
    }
    void DisplayTime()
    {
        int hours = Mathf.FloorToInt(timer / 3600.0f);
        int minutes = Mathf.FloorToInt((timer - hours * 60) / 60);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        text_Timer.text = "Congratulation, You've escaped!\nClear Time:" + " " +  string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }


    public void Retstart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
