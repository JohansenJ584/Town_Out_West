using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogCount : MonoBehaviour
{
    Text TextValue;
    int amountLog = 0;
    // Start is called before the first frame update
    private void Start()
    {
        TextValue = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        TextValue.text = amountLog + "";
    }

    public void ChangeLogCount(int amount)
    {
        amountLog += amount;
    }

    public bool IsLogLeft()
    {
        return (amountLog > 0);
    }
    public int GetLogCount()
    {
        return amountLog;
    }
}
