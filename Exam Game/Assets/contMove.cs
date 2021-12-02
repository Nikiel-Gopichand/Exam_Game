using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contMove : MonoBehaviour
{
    public float liveTime=5;
    bool on;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(die());
        StartCoroutine(onon());
    }

    // Update is called once per frame
    public IEnumerator die()
    {
        yield return new WaitForSeconds(liveTime);
        GameObject.Destroy(this.gameObject);
    }
    public IEnumerator onon()
    {
        yield return new WaitForSeconds(0.02f);
        on = true;
    }
    void Update()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 10f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (on == true)
        {
            GameObject.Destroy(this.gameObject);
        }
        
    }
}
