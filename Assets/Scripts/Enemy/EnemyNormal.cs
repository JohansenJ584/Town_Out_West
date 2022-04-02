using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNormal : MonoBehaviour
{
    public float lookRadius = 10f;
    public float WanderRadiusMax = 5f;
    public float wanderTimerMax = 5f;
    public float EnemyHealth = 50f;

    //public GameObject Coins;
    public DropTable TheDropTable;

    public bool DiedBool = false;

    protected Transform target;
    protected NavMeshAgent agent;
    protected float timer;
    protected float wanderTimer;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = Random.Range(1f, wanderTimerMax);
        TheDropTable.MakeDic();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
        }
        else if (timer >= wanderTimer)
        {
            //Random movement
            Vector3 newPos = RandomMove(transform.position, 1f);
            agent.SetDestination(newPos);
            timer = 0;
            wanderTimer = Random.Range(1f, wanderTimerMax);
        }
    }

    private void OnTriggerEnter(Collider collision) //OnCollisionEnter
    {
        //Debug.Log("I TOOK DAMAGE");
        WeopenStats tempStats = collision.gameObject.GetComponent<WeopenStats>();
        if (tempStats != null)
        {
            Debug.Log("I TOOK DAMAGE");
            EnemyHealth -= tempStats.DamageAmount;
        }
        if(EnemyHealth <= 0f)
        {
            if (!DiedBool)
            {
                DiedBool = true;
                OnDealth(TheDropTable, transform);
            }
        }
    }

    protected Vector3 RandomMove(Vector3 origin, float WanderScaler)
    {
        int layermask = -1;
        Vector3 randDirection = Random.insideUnitSphere * Random.Range(0, WanderRadiusMax) * WanderScaler;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, WanderRadiusMax, layermask);
        return navHit.position;
    }

    protected virtual void OnDealth(DropTable dropTable, Transform origin)      //(Dictionary<GameObject, float> dropChanceList, Transform origin)
    {
        //Debug.Log(dropTable.dropChanceDic);
        foreach (var pair in dropTable.dropChanceDic)
        {
            //Debug.Log(pair.Key);
            if (Random.Range(0f, 1f) <= pair.Value)
            {
                //Debug.Log(pair.Key);
                GameObject tempobject = Instantiate(pair.Key);
                tempobject.transform.position = origin.position + new Vector3(0,2f,0);
                tempobject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-3f,3f) ,30f, Random.Range(-3f, 3f)));
            }
        }
        Destroy(origin.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
