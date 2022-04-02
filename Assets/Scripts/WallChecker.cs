using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallChecker : MonoBehaviour
{
    public List<GameObject> rockWallsList = new List<GameObject>();


    public GameObject WhatSpawnedMe = null;
    private void Start()
    {
        Invoke("MakeSmall", .1f);
        Invoke("MakeNotTriggerAndBigAddRock", .5f);
    }
    void MakeSmall()
    {
        gameObject.GetComponent<BoxCollider>().size = new Vector3(.9f, 0f, .9f);
    }
    void MakeNotTriggerAndBigAddRock()
    {
        gameObject.GetComponent<BoxCollider>().isTrigger = false;
        gameObject.GetComponent<BoxCollider>().size = new Vector3(1f, 0f, 1f);

        GameObject tempRock = Instantiate(rockWallsList[Random.Range(0, 4)], transform.position, new Quaternion());
        Vector3 tempPos2 = tempRock.transform.position;
        tempPos2.y = Terrain.activeTerrain.SampleHeight(tempRock.transform.position) + 1.3f;
        tempRock.transform.position = tempPos2;
        tempRock.transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);
    }
    // Start is called before the first frame update
    //private void OnCollisionEnter(Collision collision)
    //{
        /*
        Debug.Log(WhatSpawnedMe);
        if (collision.gameObject != WhatSpawnedMe && collision.gameObject.tag != "Player")
        {
            Destroy(gameObject);
            if (collision.gameObject.name.Substring(0, 5) == "Wall1")
            {
                Destroy(collision.gameObject);
            }
        }
        */
    //}
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(WhatSpawnedMe + "Touched" + other.gameObject);
        if (other.gameObject != WhatSpawnedMe && (other.gameObject.tag == "Room" || other.gameObject.tag == "Hallway" || other.gameObject.tag == "Hallway_Norm" || other.gameObject.tag == "Wall"))
        {
            Destroy(gameObject);
            if (other.gameObject.name.Substring(0, 5) == "Wall1")
            {
                Destroy(other.gameObject);
            }
        }
    }
}
