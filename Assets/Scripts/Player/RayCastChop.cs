using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastChop : MonoBehaviour
{
    treeManager treeScript;
    public int rayLength = 5;
    WeoponManager playerMang;

    float curTime = 0;
    float waitFor = 1.2f;

    GameObject tempCanvas = null;

    // Start is called before the first frame update
    void Start()
    {
        playerMang = PlayerManager.instance.player.transform.GetComponentInChildren<WeoponManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        if(Physics.Raycast(transform.position, fwd, out hit, rayLength))
        {
            if (hit.collider.gameObject.tag == "Tree")
            {
                if (tempCanvas == null || !tempCanvas.Equals(hit.collider.gameObject.transform.GetChild(0).gameObject))
                {
                    tempCanvas = hit.collider.gameObject.transform.GetChild(0).gameObject;
                    tempCanvas.SetActive(true);
                }
                //treeScript = GameObject.Find(hit.collider.gameObject.name).GetComponent<treeManager>();
                treeScript = hit.collider.gameObject.GetComponent<treeManager>();
                if (Input.GetButton("Fire1") && playerMang.GetAxeSwing() && Time.time - curTime >= waitFor)
                {
                    curTime = Time.time;
                    //print("Hit");
                    Invoke("DoLateTree", .5f); //Same time as particle
                    //treeScript.AxeHitTree(26);
                    //This is temp for know before the particles are on 
                    treeScript.SpawnParticles(hit.point);
                    //print("test3");
                }
            }
        }
        else
        {
            if (tempCanvas != null)
            {
                tempCanvas.SetActive(false);
                tempCanvas = null;
            }
        }
    }

    void DoLateTree()
    {
        treeScript.AxeHitTree(26);
    }
}
