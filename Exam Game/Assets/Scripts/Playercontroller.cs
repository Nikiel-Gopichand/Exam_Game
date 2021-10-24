using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public Animator anim;
    public int speed = 3;
    public int sps = 6;
    public GameObject camPivot;
    public Vector2 turn;
    public float sensitivity = 2f;
    public Collider sword;
    Vector3 runV;
    // Start is called before the first frame update
    void Start()
    {
        sword.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        runV = camPivot.transform.forward * Input.GetAxis("Vertical") + camPivot.transform.right * Input.GetAxis("Horizontal");
        runV.y = 0f;
        if (sword.enabled == false)
        {


            this.GetComponent<Rigidbody>().velocity = runV * (Input.GetKey(KeyCode.LeftShift) == true ? sps : speed);
        }
        if (Input.GetMouseButtonDown(0))
        {
            sword.enabled = true;
            anim.SetInteger("atk", (anim.GetInteger("atk") == 0 ? 1 : anim.GetInteger("atk") == 1 ? 2 : 0));
            anim.SetTrigger("attack");
            StartCoroutine("swordAttack");
           
        }
        if (GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            anim.SetFloat("Running",1);
            Vector3 lDir = transform.position + runV;
            this.transform.forward = Vector3.Lerp(transform.forward, runV, 40f * Time.deltaTime);
           
        }
        if (GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            anim.SetFloat("Running", 0);
        }
    }

    public IEnumerator swordAttack() {

        yield return new WaitForSeconds(1);
        sword.enabled = false;
    }
}
