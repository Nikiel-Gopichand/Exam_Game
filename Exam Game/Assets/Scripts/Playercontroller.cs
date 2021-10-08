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
        

        runV = camPivot.transform.forward * Input.GetAxis("Vertical") + camPivot.transform.right * Input.GetAxis("Horizontal");
        runV.y = 0f;
        this.GetComponent<Rigidbody>().velocity = runV*speed;
        if (Input.GetMouseButtonDown(0))
        {
           
            anim.SetTrigger("attack");
        }
        if (GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            anim.SetFloat("Running",1);
            Vector3 lDir = transform.position + runV;
            this.transform.forward = Vector3.Lerp(transform.forward, runV, 20f * Time.deltaTime);
           
        }
        if (GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            anim.SetFloat("Running", 0);
        }
    }
}
