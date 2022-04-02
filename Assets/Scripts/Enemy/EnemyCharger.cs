using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCharger : EnemyNormal
{
    //public float lookRadius = 10f;
    bool isCharging = false;
    bool spotted = true;
    Transform chargeTarget;
    //public GameObject Coins; //This is going to go

    float curTime = 0;
    float waitForCharge = 4.0f;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        wanderTimer = Random.Range(1f, wanderTimerMax);
        TheDropTable.MakeDic();
        chargeTarget = target;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius && !isCharging)
        {
            if(!spotted)
            {
               spotted = true;
               curTime = Time.time;
            }
            if(Time.time - curTime >= waitForCharge && !isCharging)
            {
                isCharging = true;
                chargeTarget = target;
                ChargeTarget();
            }
            else
            {
                agent.SetDestination(target.position);
            }
        }
        else if (!isCharging && spotted)
        {
           spotted = false;
           agent.SetDestination(transform.position);
        }
        else if (isCharging && agent.velocity.Equals(new Vector3(0,0,0)))
        {
            curTime = Time.time;
            isCharging = false;
            agent.speed = 5;
            agent.acceleration = 20;
        }
        else if (!isCharging && timer >= wanderTimer)
        {
            Vector3 newPos = RandomMove(transform.position, 10f);
            agent.SetDestination(newPos);
            timer = 0;
            wanderTimer = Random.Range(1f, wanderTimerMax);
        }
    }
    void ChargeTarget()
    {
        agent.speed = 35;
        agent.acceleration = 40;
        Vector3 finalDirection = (chargeTarget.position - transform.position).normalized;
        Vector3 forceTarget = transform.position + finalDirection * 50f;

        agent.SetDestination(forceTarget);
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
    //This should be overwriten
    //protected override void BeforeDealth()
    //{
     //   Dictionary<GameObject, float> tempDict = new Dictionary<GameObject, float>();
    //    tempDict.Add(Coins, 1.0f);
        //tempDict.Add();
        //OnDealth(tempDict, transform); //There might be modivers sent so no more dict
    //    Destroy(gameObject);
    //}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
