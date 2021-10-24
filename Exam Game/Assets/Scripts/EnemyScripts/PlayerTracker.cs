using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public GameObject questCanvas;

    #region Singleton
    public static PlayerTracker instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    // Start is called before the first frame update
    public GameObject player;
    public GameObject camera;
    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
        if (questCanvas!=null) { questCanvas.SetActive(false); }
        
    }
}
