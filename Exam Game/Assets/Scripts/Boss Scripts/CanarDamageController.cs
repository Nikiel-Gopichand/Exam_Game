using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanarDamageController : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public float dmg;
    public PlayerStats ps;
    public float dmgOverTime;
    public float startTime;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ps = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "swordAttack")
        {
            dmg = ps.swordDamage;

            enemy.GetComponent<CanarController>().TakeDamage(dmg);//insert reference to current player damage here.This is passed to the enemy controller to change hp
        }

        if (other.tag == "iceAttack")
        {
            startTime = Time.time;
            dmg = ps.baseFreezeDamage;

            enemy.GetComponent<CanarController>().TakeDamage(dmg);
            enemy.GetComponent<CanarController>().slowed(ps.freezeSlowMultiplier, ps.freezeTime);
        }

        if (other.tag == "fireAttack")
        {
            startTime = Time.time;
            dmg = ps.baseBurnDamage;
            dmgOverTime = dmg * ps.burnDamageMultiplier;
            //     while (Time.time<startTime+ps.burnTime) {

            //    enemy.GetComponent<EnemyMovementController>().TakeDamage(dmgOverTime);
            //   }

            enemy.GetComponent<CanarController>().TakeDamage(dmg);
        }
    }

}
