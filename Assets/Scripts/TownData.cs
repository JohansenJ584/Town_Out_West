using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TownData
{
    public static List<LogDepositScript> LogsScript= new List<LogDepositScript>();
    //public static List<int> tempLogsValue = new List<int>();
    public int[] AmountOfLogs;
    //public LogDepositScript[] LogsValueArray;
    //Will Later get stats
    public TownData()
    {
        AmountOfLogs = new int[LogsScript.Count];
        for (int i = 0; i < LogsScript.Count; i++)
        {
            AmountOfLogs[i] = LogsScript[i].GetLogCount();
        }
    }

    public static void AddLogDepositScript(LogDepositScript tempScript)
    {
        LogsScript.Add(tempScript);
    }

    public static void LoadValues(int[] tempAmount)
    {
        for (int i = 0; i < LogsScript.Count; i++)
        {
            if (LogsScript[i] != null && tempAmount.Length > i)
            {
                LogsScript[i].LoadLogCount(tempAmount[i]);
            }
        }
    }
}
