using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpitter : EnemyNormal
{
    //public float lookRadius = 10f;
    public GameObject spitObject;
    public Transform spitLaunch;
    //Transform target;
    //NavMeshAgent agent;

    float curTime = 0;
    float waitForSpit = 4.0f;
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
            if (Time.time - curTime >= waitForSpit)
            {
                curTime = Time.time;
                ThrowSpit();
            }
            else
            {
                agent.SetDestination(target.position);
            }
        }
        else if (timer >= wanderTimer)
        {
            //Debug.Log("What is this");
            //Random movement
            Vector3 newPos = RandomMove(transform.position, 3f);
            agent.SetDestination(newPos);
            timer = 0;
            wanderTimer = Random.Range(1f, wanderTimerMax);
        }
    }

    private void OnTriggerEnter(Collider collision) //OnCollisionEnter
    {
        WeopenStats tempStats = collision.gameObject.GetComponent<WeopenStats>();
        if (tempStats != null)
        {
            EnemyHealth -= tempStats.DamageAmount;
        }
        if (EnemyHealth <= 0f)
        {
            if (!DiedBool)
            {
                DiedBool = true;
                OnDealth(TheDropTable, transform);
            }
        }
    }

    void ThrowSpit()
    {
        GameObject spit = Instantiate(spitObject, spitLaunch.position, spitLaunch.rotation);
        Vector3 direction = target.transform.position - spitLaunch.position;
        spit.GetComponent<Rigidbody>().AddForce(direction.normalized * 500f);
        spit.GetComponent<Rigidbody>().AddForce(Vector3.up * 350f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
