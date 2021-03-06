using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueQuest : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index = 0;
    public float typeSpeed = 0.02f;
    public int interactRange = 3;
    public GameObject continueBtn;
    public Playercontroller playerScript;
    public Transform playerModel;
    public GameObject DialogueWindow;
    private bool opened = false;
    private bool finishedLine = false;
    public Text interactText;
    public QuestGiver QuestGiver;
    bool read = false;
    // Start is called before the first frame update
    void Start()
    {
        DialogueWindow.SetActive(false);
        playerModel = PlayerTracker.instance.player.transform;
        playerScript = PlayerTracker.instance.player.GetComponent<Playercontroller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textDisplay.text == sentences[index]) {
            if ( Input.GetKeyDown(KeyCode.Mouse0))
            {

                nextSentence();
            }

            continueBtn.SetActive(true);
        }
        float distance = Vector3.Distance(playerModel.position, transform.position);
        if (distance <= interactRange && Input.GetKeyDown(KeyCode.E) && opened == false)
        { DialogueWindow.SetActive(false);
            DialogueWindowLaunch();


        } else if (distance <= interactRange && Input.GetKeyDown(KeyCode.E) && opened == true) {

            QuestGiver.QuestWindowLaunch();
        }
   

    }
    IEnumerator Type() {
       
      
        foreach (char letter in sentences[index].ToCharArray()) {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typeSpeed);
           
        }

    }
    public void nextSentence() {
        continueBtn.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
           
            StartCoroutine(Type());

        }
        else { textDisplay.text = "";
            continueBtn.SetActive(false);
            DialogueWindow.SetActive(false);
         //   PlayerTracker.instance.camera.GetComponent<CamRot>().enabled = true;
           // playerScript.enabled = true;
           // Cursor.visible = false;
          //  interactText.enabled = true;
            opened = true;
           
            //open quest window here
            QuestGiver.QuestWindowLaunch();
        }

    }
    public void DialogueWindowLaunch(){
        opened = true;
        DialogueWindow.SetActive(true);
        PlayerTracker.instance.camera.GetComponent<CamRot>().enabled = false;
        playerScript.enabled = false;
        Cursor.visible = true;
        interactText.enabled = false;
        nextSentence();
    }
    private void OnTriggerEnter(Collider other)
    {
        interactText.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    {
        interactText.enabled = false;
    }


}
