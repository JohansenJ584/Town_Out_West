using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinStats : MonoBehaviour
{
    public float Value;
    void Start()
    {
        Value = Random.Range(1f, 100f);
        Value = (float)Mathf.Round(Value * 100f) / 100f;
    }
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 20f);
    }
    public void PickUp()
    {
        Destroy(gameObject);
    }
}
