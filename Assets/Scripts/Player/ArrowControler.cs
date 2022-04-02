using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowControler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.forward = Vector3.Slerp(gameObject.transform.forward, gameObject.GetComponent<Rigidbody>().velocity.normalized, 20 * Time.deltaTime);
    }

    void OnCollisionEnter(Collision Col)
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        //Invoke("WaitDestory", 0.1f);
        //Destroy(gameObject);
        //transform.parent = Col.transform;
    }

    void WaitDestory()
    {
        //Destroy(gameObject);
    }
}