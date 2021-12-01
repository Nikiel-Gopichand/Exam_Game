using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class softLock : MonoBehaviour
{
    public GameObject lockk;
    public float tempDist;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        tempDist = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (tempDist == 0)
            {
                lockk = other.gameObject;
                tempDist = Vector3.Distance(player.transform.position, other.transform.position);
            }
            if (Vector3.Distance(player.transform.position,other.transform.position) < tempDist)
            {
                lockk = other.gameObject;
                tempDist = Vector3.Distance(player.transform.position, other.transform.position);
            }
        }
    }
}
