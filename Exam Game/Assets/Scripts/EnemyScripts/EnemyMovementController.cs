using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovementController : MonoBehaviour
{
    public PlayerTracker playerTracker;
    public NavMeshAgent enemy;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;
    public float currentSpeed;
    public float defaultSpeed;

    public float timeBetweenAttacks;

    bool alreadyAttacked;

    public Animator enemyAnim;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    public float enemyHP=100;
    public float damageThreshhold=39;
    public bool dying=false;
  public  bool ranged = false;
    public GameObject projectile;
    public GameObject projectileSpawnPoint;
    private bool damageable=true;
    // Start is called before the first frame update

    //damageColliders
    public Collider rightArmCollider;
    public Collider leftArmCollider;//for dealing damage
    public Collider bodyCollider;//taking damage


    void Awake()
    {
        defaultSpeed = gameObject.GetComponent<NavMeshAgent>().speed;
        if (rightArmCollider!=null&&leftArmCollider!=null)
        {
            rightArmCollider.GetComponent<BoxCollider>().enabled = false;
            leftArmCollider.GetComponent<BoxCollider>().enabled = false;
       
        }
       
        player = playerTracker.GetComponent<PlayerTracker>().player.transform;
        enemy = GetComponent<NavMeshAgent>();
        if (projectile!=null) { projectile.SetActive(false); 
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);


        if (!playerInSightRange && !playerInAttackRange && dying==false ) 
        {
            enemy.speed = 1.25f;
            Patroling();
            enemyAnim.SetBool("Patroling", true);
            enemyAnim.SetBool("SightRange", false);
        }

        if (playerInSightRange && !playerInAttackRange && dying == false) 
        {
            enemy.speed = 3f;
            Chasing(); 
        }
        if (playerInSightRange && playerInAttackRange && dying == false)
        { 
            
            Attacking(); 
        
        }


        if (alreadyAttacked==true) { enemy.SetDestination(transform.position); }


        
    }
    private void Patroling()
    {
        if (rightArmCollider != null && leftArmCollider != null)
        {
            rightArmCollider.GetComponent<BoxCollider>().enabled = false;
            leftArmCollider.GetComponent<BoxCollider>().enabled = false;
        }
        enemyAnim.SetBool("Patroling", true);
        if (!walkPointSet) SearchWalkPoint();
        if (walkPointSet) {
            enemy.SetDestination(walkPoint);
           
        }
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 2f) {
           // walkPointSet = false;
            enemyAnim.SetBool("Patroling", false);
            walkPointSet = false;
          //  Invoke(nameof(IdleEnd), 3f);
            
        
        }
    }
    private void SearchWalkPoint()
    {
        if (rightArmCollider != null && leftArmCollider != null)
            {
            rightArmCollider.GetComponent<BoxCollider>().enabled = false;
            leftArmCollider.GetComponent<BoxCollider>().enabled = false;
        }
        float randZ = Random.Range(-walkPointRange, walkPointRange);
        float randX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ);
        if (Physics.Raycast(walkPoint,-transform.up,2f,whatIsGround)) {
            walkPointSet = true;
        }
    }
    private void Attacking()
    {
        
        enemyAnim.SetBool("SightRange", true);
        enemyAnim.SetBool("Patroling",false);
        enemy.SetDestination(transform.position);
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);


        if (!alreadyAttacked) {
            if (ranged == false)
            {
                alreadyAttacked = true;
                enemyAnim.SetInteger("AttackAnim", Random.Range(1, 3));
                enemyAnim.SetBool("Attacking", true);
                if (rightArmCollider != null && leftArmCollider != null)
                {
                    rightArmCollider.GetComponent<BoxCollider>().enabled = true;
                    leftArmCollider.GetComponent<BoxCollider>().enabled = true;
                }
                
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            } else if (ranged==true) {
                
                alreadyAttacked = true;
                enemyAnim.SetInteger("AttackAnim", 1);
                enemyAnim.SetBool("Attacking", true);
            
                Invoke(nameof(ResetAttack), timeBetweenAttacks);
            }
           
        }
    }
    public void ResetAttack() {
       
        if (rightArmCollider != null && leftArmCollider != null)
        {
            rightArmCollider.GetComponent<BoxCollider>().enabled = false;
            leftArmCollider.GetComponent<BoxCollider>().enabled = false;

        }
        enemyAnim.SetBool("Attacking", false);
        alreadyAttacked = false;
     
    }
    private void Chasing()
    {
        if (rightArmCollider != null && leftArmCollider != null)
        {
            rightArmCollider.GetComponent<BoxCollider>().enabled = false;
            leftArmCollider.GetComponent<BoxCollider>().enabled = false;
        }
        enemyAnim.SetBool("Attacking", false);
        enemyAnim.SetBool("SightRange", true);
        enemyAnim.SetBool("Patroling", false);

        enemy.SetDestination(player.position);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
    private void IdleEnd() {

      //  walkPointSet = false;
    }
    public void TakeDamage(float damage) 
    {
        if (damage >= damageThreshhold && damageable==true)
        {
            if (rightArmCollider != null && leftArmCollider != null)
            {
                rightArmCollider.GetComponent<BoxCollider>().enabled = false;
                leftArmCollider.GetComponent<BoxCollider>().enabled = false;
            }
            enemyAnim.SetBool("Injured", true);
            enemyHP = enemyHP - damage;
            damageable = false;
            Invoke(nameof(resetDamageable), 0.5f);
            if (enemyHP <= 0)
            {
                dying = true;
                enemyDeath();
            }
            else { StartCoroutine("injuryImmunity"); }

         
        }
        else if(damage < damageThreshhold && damageable == true)
        {
            enemyHP = enemyHP - damage;
            damageable = false;
            Invoke(nameof(resetDamageable), 0.5f);
                 if (enemyHP <= 0)
             {
                dying = true;
                enemyDeath();
            }
        }



    }
    IEnumerator injuryImmunity()
    {

        yield return new WaitForSeconds(2f);
        enemyAnim.SetBool("Injured", false);

    }
    private void enemyDeath() {
        enemyAnim.SetBool("Dead", true);
        Invoke(nameof(despawn), 3.5f);
    
    }

    private void despawn() {
        Destroy(this.gameObject);
    }

    private void FireProjectile() {

        Rigidbody rb = Instantiate(projectile, projectileSpawnPoint.transform.position, Quaternion.identity).GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
        rb.AddForce(transform.up * 8f, ForceMode.Impulse);
    }

    void resetDamageable() {

        damageable = true;
    }

    public void slowed(float slowPercent, float slowTime) { 
        //slows speed by slowPercent. e.g 0.1 means slowed 10%
    currentSpeed=(1-slowPercent)* gameObject.GetComponent<NavMeshAgent>().speed;
        gameObject.GetComponent<NavMeshAgent>().speed=currentSpeed;
        StartCoroutine(resetPlayerSpeed());
        Invoke(nameof(resetPlayerSpeed),slowTime);
    }
    public IEnumerator resetPlayerSpeed()
    {
       
         gameObject.GetComponent<NavMeshAgent>().speed=defaultSpeed;
        yield return new WaitForSeconds(3f);
    } 
}
