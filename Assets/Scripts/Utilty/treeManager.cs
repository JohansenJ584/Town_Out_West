using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeManager : MonoBehaviour
{
    int treeHealth = 100;
    int speed = 50;
    bool treeDone = false;
    public Transform log;
    public GameObject tree;
    Rigidbody ridgy;
    public GameObject chopParticles;

    public HealthBar HealthBarUI;

    // Start is called before the first frame update
    void Start()
    {
        tree = this.gameObject;
        ridgy = this.GetComponent<Rigidbody>();
        ridgy.isKinematic = true;
        HealthBarUI.SetMaxHealth(treeHealth);
    }

    public void AxeHitTree(int damage)
    {
        treeHealth -= damage;
        HealthBarUI.SetHealth(treeHealth);
    }

    //This a tempTest Before I make an abstract class and put it on all item equips
    public void SpawnParticles(Vector3 coords)
    {
        Instantiate(chopParticles, coords, new Quaternion());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (treeHealth <= 0 && !treeDone)
        {
            ridgy.isKinematic = false;
            //ridgy.AddForce(transform.forward * speed * Time.deltaTime);

            Vector3 place = ridgy.transform.position + new Vector3(0,1,0);
            //ridgy.AddForceAtPosition(transform.forward * speed, place);

            ridgy.AddRelativeForce(new Vector3(1, 2, 0f) * speed, ForceMode.Force);
            HealthBarUI.gameObject.transform.parent.gameObject.SetActive(false);

            treeDone = true;
            Invoke("DestroyTree", 7f);
        }
    }

    void DestroyTree()
    {
        Transform tempLog;
        Destroy(tree);
        //This most likely will change in amount and size but for now this is it
        Vector3 treePosition = new Vector3(0f, .05f, 0f);//(Random.Range(-1.0f, 1.0f), 1f, Random.Range(-1.0f, 1.0f));
        float randDevation = Random.Range(-.05f, .05f);
        //Debug.Log(tree.GetComponent<MeshFilter>().mesh.vertices.Length);
        tempLog = Instantiate(log, transform.GetChild(0).transform.position + treePosition, transform.rotation);//Quaternion.identity);
        //tempLog.transform.rotation =  //new Quaternion(0.707106829f, randDevation, 0, 0.707106829f);

        randDevation = Random.Range(-.05f, .05f);
        tempLog = Instantiate(log, transform.GetChild(1).transform.position + treePosition, transform.rotation);//Quaternion.identity);
        //tempLog.transform.rotation = new Quaternion(0.707106829f, randDevation, 0, 0.707106829f);

        randDevation = Random.Range(-.05f, .05f);
        tempLog = Instantiate(log, transform.GetChild(2).transform.position + treePosition, transform.rotation);//Quaternion.identity);
        //tempLog.transform.rotation = new Quaternion(0.707106829f, randDevation, 0, 0.707106829f); ;
    }

}
