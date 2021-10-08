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

        runV = camPivot.transform.forward * Input.GetAxis("Vertical") + camPivot.transform.right * Input.GetAxis("Horizontal");
        runV.y = 0f;
        this.GetComponent<Rigidbody>().velocity = runV*speed;
        if (Input.GetMouseButton(0))
        {
            anim.SetInteger("atk", (anim.GetInteger("atk") == 0 ? 1 : anim.GetInteger("atk") == 1 ? 2 : 0));
            anim.SetTrigger("attack");
        }
        if (GetComponent<Rigidbody>().velocity.magnitude != 0)
        {

            Vector3 lDir = transform.position + runV;
            this.transform.forward = Vector3.Lerp(transform.forward, runV, 20f * Time.deltaTime);
           
        }
    }
}
