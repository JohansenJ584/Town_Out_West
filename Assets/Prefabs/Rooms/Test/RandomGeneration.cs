using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NavMeshBuilder = UnityEngine.AI.NavMeshBuilder;

public class RandomGeneration : MonoBehaviour
{
    #region Singleton
    public static RandomGeneration instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("There are more than one RandomGeneration");
            return;
        }
        instance = this;
    }
    #endregion


    public LevelData levelData;
    [Range(1,100)]
    public int amountOfAttemptedRooms;
    public Vector2 XRange;
    public Vector2 ZRange;
    public int Multiply_Value = 1;

    public List<GameObject> allRooms = new List<GameObject>();
    public List<GameObject> allFloors = new List<GameObject>();

    public bool AllDone = false;

    //List<NavMeshSurface> surfaces = new List<NavMeshSurface>();
    NavMeshSurface surface;

    private void Start()
    {
        //AllDone = false;
        mainGeneration();
        Invoke("GenerateAllWalls", 1f);
        //Invoke("FinshNavMeshReMake", 1.5f);
        Invoke("Foilage", 1.5f);
        Invoke("FinshNavMeshReMake", 2f);
    }

    void Foilage()
    {
        foreach (GameObject tempRoom in allRooms)
        {
            tempRoom.GetComponent<RoomGeneration>().SpawnGrass();
            tempRoom.transform.parent = gameObject.transform;
            tempRoom.GetComponent<RoomGeneration>().SetParents();
        }
        foreach (GameObject tempFloor in allFloors)
        {
            tempFloor.GetComponent<FloorGeneration>().SpawnGrass();
            tempFloor.transform.parent = gameObject.transform;
        }
        this.gameObject.GetComponent<RandomGeneration>().enabled = false;
    }

    void GenerateAllWalls()
    {
        foreach (GameObject tempRoom in allRooms)
        {
            if (tempRoom != null)
            {
                tempRoom.GetComponent<RoomGeneration>().MakeWalls();
            }
        }

        foreach (GameObject tempFloor in allFloors)
        {
            if (tempFloor != null)
            {
                tempFloor.GetComponent<FloorGeneration>().enabled = true;
                tempFloor.GetComponent<FloorGeneration>().CanMakeNewFloor = false;
                tempFloor.GetComponent<FloorGeneration>().GenerateWalls();
                tempFloor.GetComponent<FloorGeneration>().GenerateTorch();
                tempFloor.GetComponent<FloorGeneration>().transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
            }
        }
    }

    void FinshNavMeshReMake()
    {
        foreach (GameObject tempRoom in allRooms)
        {
            //Debug.Log(tempRoom.name.Substring(0, tempRoom.name.Length - 8));
            if (!tempRoom.name.Substring(0, tempRoom.name.Length - 8).Equals("Start") || !tempRoom.name.Substring(0, tempRoom.name.Length - 8).Equals("End"))
            {
                Destroy(tempRoom);
            }
        }
        foreach (GameObject tempfloor in allFloors)
        {
            Destroy(tempfloor);
        }
        //for (int i = 0; i < allRooms.Count; i++)
        //{
            //surfaces(Terrain.activeTerrain.GetComponent<NavMeshSurface>());
        //}
        surface = Terrain.activeTerrain.GetComponent<NavMeshSurface>();

        surface.BuildNavMesh();
        //AllDone = true;

    }
    public void mainGeneration()
    {
        //Deletes Old Level
        foreach(GameObject thing1 in allRooms)
        {
            DestroyImmediate(thing1);
        }
        foreach(GameObject thing2 in allFloors)
        {
            DestroyImmediate(thing2);
        }

        float randomRotation = Random.Range(0, 4) * 90f;
        float[] randMove = { -5f, 5 };
        //Start
        allRooms.Add(Instantiate(levelData.StartRoom, new Vector3(-350f,0f,0f), new Quaternion()));
        //ItsFloors
        allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(randMove[Random.Range(0, 1)], 0f, allRooms[allRooms.Count - 1].GetComponent<RoomGeneration>().SidesAndDistance[0]), new Quaternion()));
        allFloors[allFloors.Count - 1].transform.Rotate(0f, 90f, 0f);
        allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(allRooms[allRooms.Count - 1].GetComponent<RoomGeneration>().SidesAndDistance[1], 0f, randMove[Random.Range(0, 1)]), new Quaternion()));
        allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(randMove[Random.Range(0, 1)], 0f, allRooms[allRooms.Count - 1].GetComponent<RoomGeneration>().SidesAndDistance[2]), new Quaternion()));
        allFloors[allFloors.Count - 1].transform.Rotate(0f, 90f, 0f);
        allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(allRooms[allRooms.Count - 1].GetComponent<RoomGeneration>().SidesAndDistance[3], 0f, randMove[Random.Range(0, 1)]), new Quaternion()));
        //End
        allRooms.Add(Instantiate(levelData.EndRoom, new Vector3(350f,0f,0f), new Quaternion()));
        //Its Floors
        allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(randMove[Random.Range(0, 1)], 0f, allRooms[allRooms.Count - 1].GetComponent<RoomGeneration>().SidesAndDistance[0]), new Quaternion()));
        allFloors[allFloors.Count - 1].transform.Rotate(0f, 90f, 0f);
        allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(allRooms[allRooms.Count - 1].GetComponent<RoomGeneration>().SidesAndDistance[1], 0f, randMove[Random.Range(0, 1)]), new Quaternion()));
        allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(randMove[Random.Range(0, 1)], 0f, allRooms[allRooms.Count - 1].GetComponent<RoomGeneration>().SidesAndDistance[2]), new Quaternion()));
        allFloors[allFloors.Count - 1].transform.Rotate(0f, 90f, 0f);
        allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(allRooms[allRooms.Count - 1].GetComponent<RoomGeneration>().SidesAndDistance[3], 0f, randMove[Random.Range(0, 1)]), new Quaternion()));

        //Creates the random rooms
        for (int i = 0; i < amountOfAttemptedRooms; i++)
        {
            randomRotation = Random.Range(0, 4) * 90f;
            RoomGeneration CurrentRoomScript;
            //need to make it ever 20 or something
            allRooms.Add(Instantiate(levelData.Rooms[Random.Range(0, levelData.Rooms.Count)]
                ,new Vector3(Random.Range((int)XRange.x, (int)XRange.y) * Multiply_Value, 0f, Random.Range((int)ZRange.x, (int)ZRange.y) * Multiply_Value), new Quaternion()));
            allRooms[allRooms.Count - 1].transform.Rotate(0f,randomRotation, 0);
            CurrentRoomScript = allRooms[allRooms.Count - 1].GetComponent<RoomGeneration>();
            if (randomRotation - 270f == 0 || randomRotation - 90f == 0)
            {
                allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(randMove[Random.Range(0,1)], 0f, CurrentRoomScript.SidesAndDistance[1]), new Quaternion()));
                allFloors[allFloors.Count - 1].transform.Rotate(0f, 90f, 0f);
                allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(CurrentRoomScript.SidesAndDistance[0], 0f, randMove[Random.Range(0, 1)]), new Quaternion()));
                allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(randMove[Random.Range(0, 1)], 0f, CurrentRoomScript.SidesAndDistance[3]), new Quaternion()));
                allFloors[allFloors.Count - 1].transform.Rotate(0f, 90f, 0f);
                allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(CurrentRoomScript.SidesAndDistance[2], 0f, randMove[Random.Range(0, 1)]), new Quaternion()));
            }
            else
            {
                allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(randMove[Random.Range(0, 1)], 0f, CurrentRoomScript.SidesAndDistance[0]), new Quaternion()));
                allFloors[allFloors.Count - 1].transform.Rotate(0f, 90f, 0f);
                allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(CurrentRoomScript.SidesAndDistance[1], 0f, randMove[Random.Range(0, 1)]), new Quaternion()));
                allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(randMove[Random.Range(0, 1)], 0f, CurrentRoomScript.SidesAndDistance[2]), new Quaternion()));
                allFloors[allFloors.Count - 1].transform.Rotate(0f, 90f, 0f);
                allFloors.Add(Instantiate(levelData.floor, allRooms[allRooms.Count - 1].transform.position + new Vector3(CurrentRoomScript.SidesAndDistance[3], 0f, randMove[Random.Range(0, 1)]), new Quaternion()));
            }
            //Room adds all the floors that are connected to it.
            //Floors add all its floors from the same room so they dont connect to eachother.
            for (int k = 0; k < 4; k++)
            {
                CurrentRoomScript.ConnectingFloors.Add(allFloors[allFloors.Count - (4-k)]);
                FloorGeneration tempScript = allFloors[allFloors.Count - (k + 1)].GetComponent<FloorGeneration>();
                tempScript.WhatDirection = (3-k);
                for (int j = 0; j < 4; j++)
                {
                    if (k != j)
                    {
                        tempScript.otherFloors.Add(allFloors[allFloors.Count - (j + 1)]);
                    }
                    else
                    {
                        tempScript.otherFloors.Add(allFloors[allFloors.Count - (j + 1)]); //This is temp to see if it fixes something
                    }
                    tempScript.RoomICameFrom = allRooms[allRooms.Count - 1];
                }
            }
            //Add the resources and monster spawn points
            CurrentRoomScript.SpawnerPreFab = levelData.SpawnerObject;
            CurrentRoomScript.MonsterList = levelData.Monster;
            CurrentRoomScript.ReasourcePreFab = levelData.ResourceObject;

            //Temp To see is moving here is going to fix them
            //allRooms[0].gameObject.transform.position = new Vector3(-350f, 0f, 0f);
            //allRooms[1].gameObject.transform.position = new Vector3(350f, 0f, 0f);
        }

    }
}
