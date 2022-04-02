using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FlyingMovAttack : EnemyNormal
{
    //public float lookRadius;
    //Transform target;
    //NavMeshAgent agent;
    //float curTime = 0;
    //float waitForCharge = 4.0f;

    public float horizontalSpeed;
    public float verticalSpeed;
    public float amplitude;
    private Vector3 tempPosition;
    // Start is called before the first frame update
    void Start()
    {
        tempPosition = transform.GetChild(0).localPosition;
        target = PlayerManager.instance.player.transform;
        wanderTimer = Random.Range(1f, wanderTimerMax);
        agent = GetComponent<NavMeshAgent>();
        TheDropTable.MakeDic();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer += Time.deltaTime;
        FlySway();
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);
        }
        else if (timer >= wanderTimer)
        {
            //Random movement
            Vector3 newPos = RandomMove(transform.position, 5f);
            agent.SetDestination(newPos);
            timer = 0;
            wanderTimer = Random.Range(1f, wanderTimerMax);
        }
        tempPosition = transform.GetChild(0).localPosition;
        FlySway();
    }

    void FlySway()
    {
        RaycastHit hit;
        Vector3 downVector = transform.TransformDirection(Vector3.down);
        if (Physics.Raycast(transform.position, downVector, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject.tag == "MainGround")
            {
                if (hit.distance <= 1.5)
                {
                    tempPosition.y += .1f;
                }
                else
                {
                    tempPosition.y = tempPosition.y + (Mathf.Sin(Time.realtimeSinceStartup * verticalSpeed) * amplitude/2);
                }
                tempPosition.x = tempPosition.x + (Mathf.Sin(Time.realtimeSinceStartup * horizontalSpeed) * amplitude / 4);
                tempPosition.z = tempPosition.z + (Mathf.Sin(Time.realtimeSinceStartup * horizontalSpeed) * amplitude / 6);
                transform.GetChild(0).localPosition = tempPosition;
            }
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
            //BeforeDealth();
            OnDealth(TheDropTable, transform);
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}