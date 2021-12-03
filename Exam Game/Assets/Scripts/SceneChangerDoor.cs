using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangerDoor : MonoBehaviour
{
    public Playercontroller playerScript;
    public Transform playerModel;
    public float interactRange = 5f;
    public int sceneNumber;
    public Text interactText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(playerModel.position, transform.position);
        if (distance <= interactRange && Input.GetKeyDown(KeyCode.E))
        {

            SceneManager.LoadScene(sceneNumber);

        }

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