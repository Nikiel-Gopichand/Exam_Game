using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Running",Input.GetAxis("Vertical"));
        this.GetComponent<Rigidbody>().velocity = new Vector3(10*Input.GetAxis("Horizontal"), 0,10* Input.GetAxis("Vertical"));
    }
}
