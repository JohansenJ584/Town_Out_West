using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Level Data", menuName = "Level/Rooms")]
public class LevelData : ScriptableObject
{
    [Header("Level Info")]
    public GameObject StartRoom;
    public GameObject EndRoom;
    public GameObject floor;
    public List<GameObject> Rooms = new List<GameObject>();
    //will have other stuff like tress and amount needed to do certian things.
    [Header("Spawner Info")]
    public GameObject SpawnerObject;
    public List<GameObject> Monster = new List<GameObject>();
    [Header("Resource Info")]
    public GameObject ResourceObject;
    [Header("Detal Info")]
    public List<GameObject> AllGrass = new List<GameObject>();

}
