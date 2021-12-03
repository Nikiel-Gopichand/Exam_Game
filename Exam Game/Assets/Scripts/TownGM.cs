using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownGM: MonoBehaviour
{
    public Canvas pauseUI;
    public bool quest1Active;
    public Playercontroller pc;
    public WaypointController wc;
    public GameObject seer1, seer2, seer3;// seers 3 dialogues

 


    // Start is called before the first frame update
    void Start()
    {
        if (seer1 != null && seer2 != null && seer3 != null) {

            seer1.SetActive(false); seer2.SetActive(false); seer3.SetActive(false);
        }
      
        
        pauseUI.enabled = false;
        
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


        if (PlayerPrefs.GetInt("CanarDefeated") == 1) {
            seer2.SetActive(true);
        } else if (PlayerPrefs.GetInt("CanarDefeated") == 1 && PlayerPrefs.GetInt("IncendoDefeated") == 1) {
            seer3.SetActive(true);
        } else if (PlayerPrefs.GetInt("CanarDefeated") != 1 && PlayerPrefs.GetInt("IncendoDefeated") != 1) { seer1.SetActive(true); }

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
