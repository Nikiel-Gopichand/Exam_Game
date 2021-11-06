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
    public float stamina = 100;
    Vector3 runV;
    bool sprinting;
    // Start is called before the first frame update
    void Start()
    {
        sprinting = false;
        sword.enabled = false;
    }
  
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprinting = false;
        }
        if (stamina < 0)
        {
            stamina = 0;
        }
        if (stamina <= 0)
        {
            sprinting = false;
        }
        runV = camPivot.transform.forward * Input.GetAxis("Vertical") + camPivot.transform.right * Input.GetAxis("Horizontal");
        runV.y = 0f;
        if (sword.enabled == false)
        {
            this.GetComponent<Rigidbody>().velocity = runV * (sprinting == true && stamina > 0 ? sps : speed);
        }
        if (GetComponent<Rigidbody>().velocity.magnitude > 0 && stamina > 0 && sprinting == true)
        {
            stamina -= 10f * Time.deltaTime;
        }
        if (stamina < 100f && sprinting == false)
        {
            stamina += 10f * Time.deltaTime;
        }
        if (stamina > 100f)
        {
            stamina = 100f;
        }
        if (Input.GetMouseButtonDown(0) && sword.enabled != true)
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
