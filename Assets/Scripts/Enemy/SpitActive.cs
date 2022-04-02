using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitActive : MonoBehaviour
{

    public GameObject explosionEffect;

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);


        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.transform.tag == "MainGround" || collision.transform.tag == "Player")
        //{
            Explode();
        //}
    }
}
