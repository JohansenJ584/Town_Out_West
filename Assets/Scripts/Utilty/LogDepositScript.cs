using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogDepositScript : MonoBehaviour
{
    TextMesh TextValue;
    int amountLog = 0;
    // Start is called before the first frame update
    private void Start()
    {
        TownData.AddLogDepositScript(this);
        TextValue = gameObject.GetComponent<TextMesh>();
    }
    public int GetLogCount()
    {
        return amountLog;
    }

    public void ChangeLogCount(int amount)
    {
        amountLog += amount;
        TextValue.text = amountLog + "";
    }

    public void LoadLogCount(int amount)
    {
        amountLog = amount;
        TextValue.text = amountLog + "";
    }
}
