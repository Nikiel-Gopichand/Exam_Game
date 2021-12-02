using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public Animator anim;
    public int speed = 3;
    public int sps = 6;
    public GameObject camPivot;
    public GameObject lockRadius;
    public Vector2 turn;
    public float sensitivity = 2f;
    public Collider sword;
    public float stamina = 100;
    Vector3 runV;
    bool sprinting;
    public PlayerStats Stats;
    public bool damageable=true;
    private Vector3 startPos;
    int clickCount = 0;
    float addWait = 0f;
    float addWait2 = 0f;
    public GameObject abilityFirePoint;
    public GameObject fireball;
    public GameObject iceblast;
    public bool fireUnlocked;
    public bool iceUnlocked;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        sprinting = false;
        sword.enabled = false;
    }
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && fireUnlocked == true)
        {
            Instantiate(fireball,abilityFirePoint.transform.position, abilityFirePoint.transform.rotation , null);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && iceUnlocked == true)
        {
            Instantiate(iceblast, abilityFirePoint.transform.position, abilityFirePoint.transform.rotation, null);
        }
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
            if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            {
                anim.SetBool("isRunning", true);
            }
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
            {
                anim.SetBool("isRunning", false);
            }
        }
        if (sword.enabled == true)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
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
        if (sword.enabled == true && Input.GetMouseButtonDown(0))
        {
            if (clickCount == 2)
            {
                addWait2 = 0.5f;
                anim.SetTrigger("attack3");
                clickCount = 0;
            }
            if (clickCount == 1)
            {
                anim.SetTrigger("attack2");
                clickCount = 2;
                addWait = 0.5f;
            }

        }
        if (Input.GetMouseButtonDown(0) && sword.enabled != true)
        {
            if (lockRadius.GetComponent<softLock>().lockk != null)
            {
                this.transform.forward = this.transform.position - lockRadius.GetComponent<softLock>().lockk.transform.position;
            }
            sword.enabled = true;
           // anim.SetInteger("atk", (anim.GetInteger("atk") == 0 ? 1 : anim.GetInteger("atk") == 1 ? 2 : 0));
            anim.SetTrigger("attack");
            clickCount = 1;
            anim.SetFloat("Running", 0);
            StartCoroutine("swordAttack");
            
           
        }
        
        if (GetComponent<Rigidbody>().velocity.magnitude != 0)
        {
            anim.SetFloat("Running",1);
            Vector3 lDir = transform.position + runV;
            this.transform.forward = Vector3.Lerp(transform.forward, runV, 200f * Time.deltaTime);
           
        }
        if (GetComponent<Rigidbody>().velocity.magnitude == 0)
        {
            anim.SetFloat("Running", 0);
        }
    }

    public IEnumerator swordAttack() {

        yield return new WaitForSeconds(0.75f);
        yield return new WaitForSeconds(addWait);
        yield return new WaitForSeconds(addWait2);
        sword.enabled = false;
        anim.SetInteger("atk", anim.GetInteger("atk") + 1);
        if (anim.GetInteger("atk") >= 3)
        {
            anim.SetInteger("atk", 0);
        }
        addWait = 0f;
        addWait2 = 0f;
        
    }
    
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Damager" && damageable == true)
        {
            damageable = false;
            Stats.damaged = true;
            Invoke(nameof(resetInvincible), 3f);
            Stats.HP -= 20;
            if (Stats.HP <= 0)
            {
                transform.position = startPos;
                Stats.HP = 100;
            }

        }
    }
    void resetInvincible() {
        damageable = true;
        Stats.damaged = false;
    }
}
