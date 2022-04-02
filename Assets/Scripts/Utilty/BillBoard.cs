using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    Transform cam;

    /// <summary>
    /// Start function grabs main camera
    /// </summary>
    private void Start()
    {
        cam = Camera.main.transform;
        //GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
    /// <summary>
    /// Late update makes sure every other update function happens first before transforms of billboards are changed
    /// </summary>
    private void LateUpdate()
    {
        transform.LookAt(new Vector3(cam.position.x, gameObject.transform.position.y, cam.position.z));
    }
}
