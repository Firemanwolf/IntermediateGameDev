using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject RestartButton;
    public TextMeshProUGUI score;
    public GameObject WinPanel;
    private int LightNum = 0;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("Ball").Length);
        if(GameObject.Find("Lights") != null) LightNum = GameObject.Find("Lights").transform.childCount;
        if(score != null)score.text = "Lights Left: " + LightNum;

        if(LightNum == 0 && score != null)
        {
            WinPanel.SetActive(true);
            Time.timeScale = 0;
        }

        if(GameObject.FindGameObjectsWithTag("Ball").Length == 0 && score != null)
        {
            Lost(RestartButton);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Lost(GameObject Restart_Btn)
    {
        Time.timeScale = 0;
        Restart_Btn.SetActive(true);
    }
}
