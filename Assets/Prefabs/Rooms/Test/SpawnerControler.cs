using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControler : MonoBehaviour
{
    public GameObject WhatCreature;
    public float lookRadius = 10f;

    protected Transform target;
    bool PlayerInScene = false;

    bool Spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerManager.instance != null)
        {
            target = PlayerManager.instance.player.transform;
            PlayerInScene = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (PlayerInScene && Spawned != true)
        {
            float distance = Vector3.Distance(target.position, transform.position);
            if (distance <= lookRadius)
            {
                //stuff happens looks cool
                Instantiate(WhatCreature, gameObject.transform.position + new Vector3(0f, 1f, 0f), new Quaternion());
                Spawned = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
