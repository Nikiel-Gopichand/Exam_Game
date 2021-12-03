using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonGM: MonoBehaviour
{
    public Canvas pauseUI;
    public bool quest1Active;
    public Playercontroller pc;
    // Start is called before the first frame update
    void Start()
    {
        pauseUI.enabled = false;
        PlayerPrefs.DeleteAll();
        if (PlayerPrefs.GetInt("CanarDefeated") == 1)
        {
            pc.iceUnlocked = true;

        }
        else { pc.iceUnlocked = false; }
        if (PlayerPrefs.GetInt("IncendoDefeated") == 1)
        {
            pc.fireUnlocked = true;
        }
        else { pc.fireUnlocked = false; }
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
