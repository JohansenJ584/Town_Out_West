using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressFToUse : MonoBehaviour
{
    bool isPickable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isPickable) //&& !gameObject.transform.GetChild(0).gameObject.activeSelf)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            //print("This is true");
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void PickableBread(bool pick)
    {
        isPickable = pick;
    }


}
