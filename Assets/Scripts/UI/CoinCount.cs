using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCount : MonoBehaviour
{
    Text TextValue;
    float amountCoin = 0;
    // Start is called before the first frame update
    private void Start()
    {
        TextValue = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        TextValue.text = amountCoin + "";
    }

    public bool ChangeCoinCount(float amount)
    {
        if(amountCoin + amount >= 0)
        {
            amountCoin += amount;
            return true;
        }
        else
        {
            Debug.Log("you dont have the money");
            return false;
        }
    }

    public float GetCoinValue()
    {
        return amountCoin;
    }
}
