using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeHeightOfTerrian : MonoBehaviour
{
    //Extra value allowing for diest hieght adjustment per object script
    public float heightAbove = 0f;
    /// <summary>
    /// Places objects on the surface of Terrian When they are loaded in
    /// </summary>
    void Start()
    {
        Vector3 tempPos = transform.position;
        tempPos.y = Terrain.activeTerrain.SampleHeight(transform.position) + heightAbove;
        transform.position = tempPos;
    }
}
