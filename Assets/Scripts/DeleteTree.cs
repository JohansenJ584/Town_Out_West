using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTree : MonoBehaviour
{
    /// <summary>
    /// Simple Way of deleting location tree in generation process
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Wall")
        {
            //gameObject.SetActive(gameObject.active);
            
            Destroy(gameObject);
        }
    }
}
