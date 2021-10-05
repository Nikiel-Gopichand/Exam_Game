using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public Animator anim;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            anim.SetBool("Walking", true);
            this.GetComponent<Rigidbody>().velocity = new Vector3(10 * Input.GetAxis("Horizontal"), 0, 10 * Input.GetAxis("Vertical"));
           
              
            
        }
        else {
            anim.SetBool("Walking", false);
        }
        if (Input.GetKey("d")) {
            anim.SetBool("RightSideStep", true);




        }
        else { anim.SetBool("RightSideStep", false); }
        if (Input.GetKey("a"))
        {
            anim.SetBool("LeftSideStep", true);

        }
        else
        {
            anim.SetBool("LeftSideStep", false);
        }

        if (Input.GetKey("s"))
        {
            anim.SetBool("BackStep", true);
          
        }
        else {anim.SetBool("BackStep", false); }

    }

}
