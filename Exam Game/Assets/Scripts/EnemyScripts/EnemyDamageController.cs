using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    public GameObject enemy;
    public float damage;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("PlayerAttack");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="PlayerAttack") {
            enemy.GetComponent<EnemyController>().Damaged(41);//insert reference to current player damage here.This is passed to the enemy controller to change hp
        }
    }
}
