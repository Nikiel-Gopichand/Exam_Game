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
    public int interactRange=5;
    public bool accepted;
    public bool finishedQuest;
    public Button turnInBtn;
    public Text errorText;
    public Button acceptBtn;
    public GameObject[] enemyArray;
    public Text interactText;


    private void Awake()
    {
        interactText.enabled = false;
        if (accepted == true)
        {

            turnInBtn.enabled = true;
            acceptBtn.enabled = false;
        }
        else { turnInBtn.enabled = false; }
    }
    private void Start()
    {
      
        accepted = false;
        finishedQuest = false;
        playerModel = PlayerTracker.instance.player.transform;
        playerScript = PlayerTracker.instance.player.GetComponent<Playercontroller>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(playerModel.position, transform.position);



        if (distance <= interactRange && Input.GetKeyDown(KeyCode.E))
        {
            QuestWindowLaunch();

        }
        if (accepted == true)
        {
            progressTracker();
        }
       

    }
    private void FixedUpdate()
    {
       
    }
    public void QuestWindowLaunch()
    {
      
        questWindow.SetActive(true);
        Cursor.visible = true;
        titleText.text = quest.title;
        descriptionText.text = quest.description;
        rewardText.text = quest.reward;
        playerScript.enabled = false;
        PlayerTracker.instance.camera.GetComponent<CamRot>().enabled = false;
        interactText.enabled = false;




    }
    public void QuitWindow()
    {
        Cursor.visible = false;
        questWindow.SetActive(false);
        playerScript.enabled = true;
        //unfreeze camera on window quit
        PlayerTracker.instance.camera.GetComponent<CamRot>().enabled = true;
        interactText.enabled = true;

    }
    public void acceptQuest() {

    
        turnInBtn.enabled = true;
        accepted = true;
        playerScript.enabled = true;
        PlayerTracker.instance.camera.GetComponent<CamRot>().enabled = true;
        acceptBtn.gameObject.SetActive(false);
        questWindow.SetActive(false);
        Cursor.visible = false;
        interactText.enabled = true;


    }

    public void progressTracker() {
        int enemCounter = 0 ;

        for (int i=0;i<enemyArray.Length-1;i++) {
            if (enemyArray[i]!=null) {
                enemCounter++;
            }
        
        }

        if (enemCounter == 0)
        {
            finishedQuest = true;
        }


    }

    public void TurnInQuest() {
        if (finishedQuest == true)
        {

            questWindow.SetActive(false);
            playerScript.enabled = true;
            //unfreeze camera on window quit
            PlayerTracker.instance.camera.GetComponent<CamRot>().enabled = true;
        }
        else {
            errorText.text = "Complete quest objective before turning in.";
        }
       
    }

    private void OnTriggerEnter(Collider other)
    {
        interactText.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        interactText.enabled = false ;
    }


}
