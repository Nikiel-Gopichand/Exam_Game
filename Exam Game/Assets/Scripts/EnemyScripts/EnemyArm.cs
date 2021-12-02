using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArm : MonoBehaviour
{
    public GameObject otherArm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag=="Player") {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            otherArm.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
