using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    // Start is called before the first frame update
    public QuestController quest;
    public Playercontroller playerScript;
    public Transform playerModel;
    public GameObject questWindow;
    public Text titleText;
    public Text descriptionText;
    public Text rewardText;
    public int interactRange=3;


    private void Start()
    {
        playerModel = PlayerTracker.instance.player.transform;
        playerScript = PlayerTracker.instance.player.GetComponent<Playercontroller>();
    }
    private void FixedUpdate()
    {
        float distance = Vector3.Distance(playerModel.position, transform.position);
        if (distance<=interactRange&& Input.GetKeyDown(KeyCode.E)) {
            QuestWindowLaunch();
        
        }
    }
    public void QuestWindowLaunch()
    {
        questWindow.SetActive(true);
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        rewardText.text = quest.reward;
        playerScript.enabled = false;
     //   PlayerTracker.instance.camera.SetActive() 
//freeze camera on quest open



       
    }
    public void QuitWindow()
    {
        questWindow.SetActive(false);
        playerScript.enabled = true;
        //unfreeze camera on window quit


    }
}
