using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool tester = false;
    public static bool gamePuased;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SaySomething", 3f, 2.5f);
    }

    // Update is called once per frame
    void Update()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(DelayedHello(5));

        if (Input.GetKeyDown(KeyCode.Space)) tester = true;
    }

    #region pausing

    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePuased = true;
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gamePuased = false;
    }
    #endregion

    void SaySomething()
    {

    }
    IEnumerator DelayedHello(float waitTime)
    {
        Debug.Log("...");

        while (!tester)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log("Hello");
        }
    }

    //pause method
    //Time timeScale = 0f
    //A paused varaible
}
