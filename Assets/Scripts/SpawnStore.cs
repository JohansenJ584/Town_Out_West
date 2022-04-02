using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStore : MonoBehaviour
{
    private LogDepositScript DepositScript;
    private bool StoreHere = false;

    public int NegativeX = 1;
    public int NegativeZ = 1;
    public int HowManyLongsDoesItTake = 3;

    public GameObject storePreFab;
    void Start()
    {
        DepositScript = gameObject.GetComponent<LogDepositScript>();
    }

    void FixedUpdate()
    {
        //Debug.Log(DepositScript.GetLogCount() == HowManyLongsDoesItTake);
        if (DepositScript.GetLogCount() >= HowManyLongsDoesItTake && !StoreHere)
        {
            StoreHere = true;
            GameObject temp = Instantiate(storePreFab, new Vector3(-10f * NegativeX, 7f, -25f * NegativeZ) + gameObject.transform.parent.position, new Quaternion());
            //Debug.Log(gameObject.transform.parent.name);
            temp.name = "Store_" + gameObject.transform.parent.name.Substring(gameObject.transform.parent.name.Length - 1, 1);
        }
    }
}
