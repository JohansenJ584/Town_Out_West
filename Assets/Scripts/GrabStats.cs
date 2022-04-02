//using System
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabStats : MonoBehaviour
{
    Text ArmoreText;
    Text SpeedText;
    Text DamageText;
    Text RangeText;
    Text GatheringText;
    Text JumpText;
    void Start()
    {
        //Armore
        ArmoreText = gameObject.transform.GetChild(0).gameObject.GetComponent<Text>();
        //Speed
        SpeedText = gameObject.transform.GetChild(1).gameObject.GetComponent<Text>();
        //Damage
        DamageText = gameObject.transform.GetChild(2).gameObject.GetComponent<Text>();
        //Range
        RangeText = gameObject.transform.GetChild(3).gameObject.GetComponent<Text>();
        //Gathering
        GatheringText = gameObject.transform.GetChild(4).gameObject.GetComponent<Text>();
        //Jump
        JumpText = gameObject.transform.GetChild(5).gameObject.GetComponent<Text>();
    }

    public void ChangeStats()
    {
        //Debug.Log(PlayerStats.instance.armor.GetValue());
        ArmoreText.text = string.Format("{0:0.00}", PlayerStats.instance.armor.GetValue()) + " %";
        SpeedText.text = string.Format("{0:0.00}", PlayerStats.instance.speed.GetValue()) + " %";
        DamageText.text = string.Format("{0:0.00}", PlayerStats.instance.damage.GetValue()) + " %";
        RangeText.text = string.Format("{0:0.00}", PlayerStats.instance.range.GetValue()) + " %";
        GatheringText.text = string.Format("{0:0.00}", PlayerStats.instance.gathering.GetValue()) + " %";
        JumpText.text = string.Format("{0:0.00}", PlayerStats.instance.jump.GetValue()) + " %";
    }
}
