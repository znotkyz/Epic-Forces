using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject impactEffect;

    public float radius = 3;

    public int damageAmount = 0;
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
        FindObjectOfType<AudioManagerScript>().Play("Rifle");

        GameObject impact = Instantiate(impactEffect, transform.position, Quaternion.identity);

        Destroy(impact, 2);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach(Collider nearbyObject in colliders)
        {
            if(nearbyObject.tag == "Player")
            {
                StartCoroutine(FindObjectOfType<PlayerManagerScript>().TakeDamage(damageAmount));
            }
        }

        this.enabled = false;
    }
}
