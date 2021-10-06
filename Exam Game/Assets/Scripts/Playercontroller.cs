using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public Animator anim;
    public int speed = 3;
    public GameObject camPivot;
    public Vector2 turn;
    public float sensitivity = 2f;
    Vector3 runV;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Running",Input.GetAxis("Vertical"));

        runV = this.transform.forward * Input.GetAxis("Vertical") + this.transform.right * Input.GetAxis("Horizontal");
        this.GetComponent<Rigidbody>().velocity = runV*speed;
       
        turn.x += Input.GetAxis("Mouse X") * sensitivity;

        transform.localRotation = Quaternion.Euler(0,turn.x, 0);

    }
}
