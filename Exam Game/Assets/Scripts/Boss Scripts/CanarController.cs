using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CanarController : MonoBehaviour
{
    public int cutsceneNumber;
    public float sightRadius = 10f;
    public float attackRange = 3f;
    Transform target;
    NavMeshAgent agent;
    public Animator enemyAnim;
    bool attacked = false;
    public float enemyHP = 500;
    public float damageThreshold = 40;
    public bool damageable=true;
    public bool dying = false;
    public float currentSpeed;
    public float defaultSpeed;
    public int stateMachine; // 0= idle; 1=chasing; 2=attack1;3=attack2;4=attack3;5=death;
    public bool enteredArena;
    public Dialogue dlg;
    public string playerprefName;
    
    // Start is called before the first frame update
    void Start()
    {
      //  dlg.enabled = false;
        defaultSpeed = gameObject.GetComponent<NavMeshAgent>().speed;
        target = PlayerTracker.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }



    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance<=sightRadius+5 &&enteredArena==false ){
            agent.SetDestination(transform.position);
           
              
                dlg.enabled = true;
                dlg.DialogueWindowLaunch();
                enteredArena = true;
           


        }


        if (distance <= sightRadius)
        {
            enemyAnim.SetInteger("AnimState", 1);
            

            FaceTarget();
            if (distance <= attackRange &&attacked == false)
            {
                agent.SetDestination(transform.position);
                enemyAnim.SetInteger("AnimState", Random.Range(2, 4));
                attacked = true;
                StartCoroutine(resetAttack());

            }
            else if (distance > attackRange)
            {
                agent.SetDestination(target.position);

                enemyAnim.SetInteger("AnimState",1);
            }
            //  agent.SetDestination(target.position); 



        }
        else
        {
            enemyAnim.SetInteger("AnimState", 0);
            agent.SetDestination(transform.position);
          
        }



    }
    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRadius);
    }

    public void Damaged(float damage)
    {
        if (damage >= damageThreshold)
        {
            enemyAnim.SetBool("Injured", true);
            enemyHP = enemyHP - damage;
            StartCoroutine("injuryImmunity");
            //if the damage taken is above a certain ammount then only play animation for injured
        }
        else
        {
            enemyHP = enemyHP - damage;

        }

    }
    IEnumerator injuryImmunity()
    {

        yield return new WaitForSeconds(1f);
        enemyAnim.SetBool("Injured", false);

    }
    IEnumerator resetAttack() {
        attacked = false;

        yield return new WaitForSeconds(3);
    
    }





    void shardStorm() { }

    void shardStormReset() { 
    // manage cooldowns
    }

    void crystalRise() { }

    void crystalRiseReset() { 
    //manage Cooldown

    
    }
    public void TakeDamage(float damage)
    {
        if ( damageable == true)
        {
            
            
            enemyHP = enemyHP - damage;
            damageable = false;
            Invoke(nameof(resetDamageable), 0.5f);
            if (enemyHP <= 0)
            {
                dying = true;
                stateMachine = 5;
                enemyAnim.SetInteger("AnimState", stateMachine);
                enemyDeath();
            }
           // else { StartCoroutine("injuryImmunity"); }


        }
      
        



    }
    private void enemyDeath()
    {
        
        Invoke(nameof(despawn), 10f);

    }

    private void despawn()
    {
        PlayerPrefs.SetInt(playerprefName,1);

        SceneManager.LoadScene(cutsceneNumber);
        Destroy(this.gameObject);
    }

    void resetDamageable()
    {

        damageable = true;
    }
    public void slowed(float slowPercent, float slowTime)
    {
        //slows speed by slowPercent. e.g 0.1 means slowed 10%
        currentSpeed = (1 - slowPercent) * gameObject.GetComponent<NavMeshAgent>().speed;
        gameObject.GetComponent<NavMeshAgent>().speed = currentSpeed;
        StartCoroutine(resetPlayerSpeed());
        Invoke(nameof(resetPlayerSpeed), slowTime);
    }
    public IEnumerator resetPlayerSpeed()
    {

        gameObject.GetComponent<NavMeshAgent>().speed = defaultSpeed;
        yield return new WaitForSeconds(3f);
    }
}
