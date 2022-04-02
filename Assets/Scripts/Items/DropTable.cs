using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New DropTable", menuName = "Inventory/DropTable")]
public class DropTable : ScriptableObject
{
    public string DropTableForWhat;
    public Dictionary<GameObject, float> dropChanceDic = new Dictionary<GameObject, float>();
    public float coinModifier; //This is dont done yet.

    [SerializeField]
    List<GameObject> keys = new List<GameObject>();
    [SerializeField]
    List<float> values = new List<float>();

    public bool wasCreated = false;

    public List<GameObject> Keys { get => keys; set => keys = value; }
    public List<float> Values { get => values; set => values = value; }

    
    public void MakeDic()
    {
       if (dropChanceDic.Count == 0)
        {
            for (int i = 0; i < Keys.Count; i++)
            {
                dropChanceDic.Add(Keys[i], Values[i]);
            }
        }
    }
    
}
