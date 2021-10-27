using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageController : MonoBehaviour
{
    public GameObject enemy;

  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="PlayerAttack") {
            enemy.GetComponent<EnemyMovementController>().TakeDamage(40f);//insert reference to current player damage here.This is passed to the enemy controller to change hp
        }
    }
}
