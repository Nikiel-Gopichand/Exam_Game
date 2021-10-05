using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookRot : MonoBehaviour
{
    public Vector2 turn;
    public float sensitivity = 2f;
    public GameObject player;
    public GameObject playerEG;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, playerEG.transform.position, 0.02f);
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;
        
        transform.localRotation = Quaternion.Euler(-turn.y, 0, 0);
    }
}
