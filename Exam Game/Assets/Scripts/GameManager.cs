using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas pauseUI;
    public bool quest1Active;
    public Playercontroller pc;
    // Start is called before the first frame update
    void Start()
    {
        pauseUI.enabled = false;
        if (PlayerPrefs.GetInt("CanarDefeated")==1)
        {
            pc.iceUnlocked = true;

        }
        if (PlayerPrefs.GetInt("IncendoDefeated")==1) 
        {
            pc.fireUnlocked = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            PlayerTracker.instance.camera.GetComponent<CamRot>().enabled = false;
            pauseUI.enabled = true;
            Time.timeScale = 0;
        }
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void unPause()
    {
        PlayerTracker.instance.camera.GetComponent<CamRot>().enabled = true;
        Cursor.visible = false;
        pauseUI.enabled = false;
        Time.timeScale = 1;

    }
}
