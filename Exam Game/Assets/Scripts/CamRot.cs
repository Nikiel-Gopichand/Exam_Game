using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRot : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Vector2 turn;
    public float sensitivity = 2f;
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.02f);
        turn.y += Input.GetAxis("Mouse Y")*sensitivity;
        turn.x += Input.GetAxis("Mouse X")*sensitivity;
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
