using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Collider rightArmCollider;
    public Collider leftArmCollider;
    public Collider bodyCollider;
    public float damageThreshold = 40; //the ammount of damage before an actual damage animation plays

    // Start is called before the first frame update
    public float sightRadius = 10f;
    public float attackRange = 3f;
    Transform target;
    NavMeshAgent agent;
    public Animator enemyAnim;
    public float enemyHP = 100;
    public float burnMultiplier = 1.2f;
    public float slowMultiplier = 0.7f;


    void Start()
    {
        target = PlayerTracker.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= sightRadius)
        {
            enemyAnim.SetBool("SightRange", true);


            FaceTarget();
            if (distance <= attackRange)
            {
                enemyAnim.SetInteger("AttackAnim", Random.Range(1, 2));
                enemyAnim.SetBool("Attacking", true);

            }
            else if (distance > attackRange)
            {
                agent.SetDestination(target.position);

                enemyAnim.SetBool("Attacking", false);
            }
            //  agent.SetDestination(target.position); 



        }
        else {
            agent.SetDestination(transform.position);
            enemyAnim.SetBool("SightRange", false);
        }



    }
    void FaceTarget() {
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
        else {
            enemyHP = enemyHP - damage;

        }

    }
    IEnumerator injuryImmunity() {

        yield return new WaitForSeconds(1f);
        enemyAnim.SetBool("Injured", false);

    }

 
 

}
