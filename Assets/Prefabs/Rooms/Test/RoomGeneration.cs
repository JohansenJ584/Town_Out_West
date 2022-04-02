using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour
{
    public bool isDestroyed = false;
    [Header("Side-in-order North-East-South-West")]
    public List<float> SidesAndDistance = new List<float>();
    public List<GameObject> ConnectingFloors = new List<GameObject>();
    [Header("RoomStuff")]
    public GameObject SpawnerPreFab;
    public GameObject ReasourcePreFab;
    public List<GameObject> MonsterList = new List<GameObject>();

    List<float> SizeOfRoom = new List<float>();
    List<GameObject> SpawnerList = new List<GameObject>();
    List<GameObject> ReasourceList = new List<GameObject>();

    public bool isSafeRoom = false;

    private Vector3 tempPost;

    List<GameObject> Walls = new List<GameObject>();
    public GameObject wallPrefab;

    List<GameObject> grassList = new List<GameObject>();

    private bool DoDeleteOthers = true;

    // Start is called before the first frame update
    void Start()
    {
        tempPost = gameObject.transform.position;


        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
        else
        {
            foreach(BoxCollider tempbox in gameObject.GetComponentsInChildren<BoxCollider>())
            {
                tempbox.isTrigger = true;
            }
        }
        Invoke("FixSize", .1f);
        foreach(float tempSide in SidesAndDistance)
        {
            if (tempSide > 0)
            {
                SizeOfRoom.Add(tempSide - 8f);
            }
            else
            {
                SizeOfRoom.Add(tempSide + 8f);
            }
        }
        if (!isSafeRoom)
        {
            MakeRoom();
        }
        //MakeWalls();
    }

    public void MakeWalls()
    {
        List<GameObject> taggedObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
        List<bool> objectNorth = new List<bool>(), objectSouth = new List<bool>(), objectEast = new List<bool>(), objectWest = new List<bool>();


        for(int i = 0; i < SidesAndDistance.Count; i++)
        {
            for(int k = 0; k < Mathf.Abs(SidesAndDistance[i])/5; k++)
            {
                if (transform.rotation.y - 90 == 0 || transform.rotation.y + 90 == 0)
                {
                    if(i == 0)
                    {
                        objectNorth.Add(true);
                    }
                    else if (i == 1)
                    {
                        objectEast.Add(true);
                    }
                    else if (i == 2)
                    {
                        objectSouth.Add(true);
                    }
                    else if (i == 3)
                    {
                        objectWest.Add(true);
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        objectEast.Add(true);
                    }
                    else if (i == 1)
                    {
                        objectNorth.Add(true);
                    }
                    else if (i == 2)
                    {
                        objectWest.Add(true);
                    }
                    else if (i == 3)
                    {
                        objectSouth.Add(true);
                    }
                }
            }
        }

        
        Vector3 position = transform.position;
        /*
        int distancex, distancez, tempdistancex, tempdistancez;
        if (transform.rotation.eulerAngles.y - 90 == 0 || transform.rotation.y + 90 == 0)
        {
            distancex = (int)SidesAndDistance[1] + 5;
            distancez = (int)SidesAndDistance[0] + 5;
        }
        else
        {
            distancex = (int)SidesAndDistance[1] + 5; //if its wierd its prop this
            distancez = (int)SidesAndDistance[0] + 5;
        }
        foreach (GameObject temp in taggedObjects)
        {
            if (transform.rotation.y - 90 == 0 || transform.rotation.y + 90 == 0)
            {
                tempdistancex = (int)temp.GetComponent<RoomGeneration>().SidesAndDistance[0] + 5;
                tempdistancez = (int)temp.GetComponent<RoomGeneration>().SidesAndDistance[1] + 5;
            }
            else
            {
                tempdistancex = (int)temp.GetComponent<RoomGeneration>().SidesAndDistance[1] + 5; //if its wierd its prop this
                tempdistancez = (int)temp.GetComponent<RoomGeneration>().SidesAndDistance[0] + 5;
            }
            //North
            //Debug.Log(position.z + "  |Mine|  " + (distancez) + "  |Compared|  " + temp.transform.position.z + "Theres" + (tempdistancez));
            if (position.z + distancez == temp.transform.position.z - tempdistancez)
            {
                //Debug.Log("1");
                if (position.x == temp.transform.position.x)
                {
                    //Debug.Log("11");
                    if (tempdistancex >= distancex)
                    {
                        for(int i = 0; i < objectNorth.Count; i++)
                        {
                            objectNorth[i] = false;
                        }
                    }
                    else //We are the same no walls
                    {
                        objectNorth[2] = false;
                        objectNorth[3] = false;
                        Debug.Log("North This is the test");
                    }
                }
                else if(position.x == temp.transform.position.x + tempdistancex || position.x == temp.transform.position.x - tempdistancex)
                {
                    objectNorth[0] = false;
                    objectNorth[1] = false;
                }
                else if (position.x + distancex == temp.transform.position.x|| position.x + distancex == temp.transform.position.x - tempdistancex)
                {
                    objectNorth[4] = false;
                    objectNorth[5] = false;

                }
                //Debug.Log("2");
            }
            //South
            if (position.z - distancez == temp.transform.position.z + tempdistancez)
            {
                //Debug.Log("South1");
                if (position.x == temp.transform.position.x)
                {
                    //Debug.Log("South2");
                    if (tempdistancex >= distancex)
                    {
                        for (int i = 0; i < objectSouth.Count; i++)
                        {
                            objectSouth[i] = false;
                        }
                    }
                    else //We are the same no walls
                    {
                        Debug.Log("South This is the test");
                        objectSouth[2] = false;
                        objectSouth[3] = false;
                    }
                }
                else if (position.x == temp.transform.position.x + tempdistancex || position.x == temp.transform.position.x - tempdistancex)
                {
                    objectSouth[0] = false;
                    objectSouth[1] = false;
                }
                else if (position.x + distancex == temp.transform.position.x || position.x + distancex == temp.transform.position.x - tempdistancex)
                {
                    objectSouth[4] = false;
                    objectSouth[5] = false;
                }
            }
            //East
            if (position.x + distancex == temp.transform.position.x - tempdistancex)
            {
                //Debug.Log("3");
                if (position.z == temp.transform.position.z)
                {
                    //Debug.Log("33");
                    if (tempdistancez >= distancez)
                    {
                        for (int i = 0; i < objectEast.Count; i++)
                        {
                            objectEast[i] = false;
                        }

                    }
                    else //We are the same no walls
                    {
                        objectEast[2] = false;
                        objectEast[3] = false;
                        Debug.Log("East This is the test");
                    }
                }
                else if(position.z == temp.transform.position.z + tempdistancez || position.z == temp.transform.position.z - tempdistancez)
                {
                    objectEast[0] = false;
                    objectEast[1] = false;

                }
                else if (position.z + distancez == temp.transform.position.z || position.z + distancez == temp.transform.position.z - tempdistancez)
                {
                    objectEast[4] = false;
                    objectEast[5] = false;

                }
            }
            //West
            if (position.x - distancex == temp.transform.position.x + tempdistancex)
            {
                if (position.z == temp.transform.position.z)
                {
                    if (tempdistancez >= distancez)
                    {
                        for (int i = 0; i < objectWest.Count; i++)
                        {
                            objectWest[i] = false;
                        }
                    }
                    else //We are the same no walls
                    {
                        objectWest[2] = false;
                        objectWest[3] = false;
                        Debug.Log("West This is the test");
                    }
                }
                else if(position.z == temp.transform.position.z + tempdistancez || position.z == temp.transform.position.z - tempdistancez)
                {
                    objectWest[0] = false;
                    objectWest[1] = false;

                }
                else if (position.z + distancez == temp.transform.position.z || position.z + distancez == temp.transform.position.z - tempdistancez)
                {
                    objectWest[4] = false;
                    objectWest[5] = false;

                }
            }
        }
        */
        GameObject wallTemp;

        //Debug.Log(SidesAndDistance.Count);
        for (int i = 0; i < SidesAndDistance.Count; i++)
        {
            int increment = 0;
            //Debug.Log((Mathf.Abs(SidesAndDistance[i]) / 5) - 1);
            for (int k = 0; k < (Mathf.Abs(SidesAndDistance[i]) / 5) - 1; k++)
            {
                //West is South and North is East and so on
                if (transform.rotation.eulerAngles.y % 180f != 0f)// || transform.rotation.eulerAngles.y + 90f == 0f)
                {
                    //Debug.Log("IN");
                    //says its easts but places north
                    if (i == 0)
                    {
                        //Debug.Log("One");
                        if (Mathf.Abs(SidesAndDistance[i]) == 15 && objectEast[k] == true)
                        {
                            if ((Mathf.Abs(SidesAndDistance[0]) == 35))
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-5f + increment, 5f, 30f) + position, new Quaternion());
                            }
                            else
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-5f + increment, 5f, 10f) + position, new Quaternion());
                            }
                            wallTemp.transform.Rotate(0f, 90f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        else if (objectEast[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(-25f + increment, 5f, 10f) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 90f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        //increment += 10;
                        //objectEast.Add(true);
                    }
                    else if (i == 1)
                    {
                        //Debug.Log("two");
                        if (Mathf.Abs(SidesAndDistance[i]) == 15 && objectNorth[k] == true)
                        {
                            if ((Mathf.Abs(SidesAndDistance[0]) == 35))
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(30f, 5f, -5f + increment) + position, new Quaternion()); //here
                            }
                            else
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(10f, 5f, -5f + increment) + position, new Quaternion());
                            }
                            wallTemp.transform.Rotate(0f, 00f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        else if (objectNorth[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(10f, 5f, -25f + increment) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 00f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        //increment += 10;
                        //objectNorth.Add(true);
                    }
                    else if (i == 2)
                    {
                        //Debug.Log("Three");
                        if (Mathf.Abs(SidesAndDistance[i]) == 15 && objectEast[k] == true)
                        {
                            if ((Mathf.Abs(SidesAndDistance[0]) == 35))
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-5f + increment, 5f, -30f) + position, new Quaternion());
                            }
                            else
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-5f + increment, 5f, -10f) + position, new Quaternion());
                            }
                            wallTemp.transform.Rotate(0f, 90f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        else if (objectEast[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(-25f + increment, 5f, -10f) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 90f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        //increment += 10;
                        //objectWest.Add(true);
                    }
                    else if (i == 3)
                    {
                        //Debug.Log("Four");
                        if (Mathf.Abs(SidesAndDistance[i]) == 15 && objectSouth[k] == true)
                        {
                            if ((Mathf.Abs(SidesAndDistance[0]) == 35))
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-30f, 5f, -5f + increment) + position, new Quaternion());
                            }
                            else
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-10f, 5f, -5f + increment) + position, new Quaternion());
                            }
                            wallTemp.transform.Rotate(0f, 00f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }

                        else if (objectSouth[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(-10f, 5f, -25f + increment) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 00f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        //increment += 10;
                        //objectSouth.Add(true)
                    }
                    increment += 10;
                }
                //North is North, West is West
                else
                {
                    if (i == 0)
                    {
                        if (Mathf.Abs(SidesAndDistance[i]) == 15 && objectEast[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(10f, 5f, -5f + increment) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 00f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        else if (objectEast[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(10f, 5f, -25f + increment) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 00f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        increment += 10;
                        //objectEast.Add(true);
                    }
                    else if (i == 1)
                    {
                        if (Mathf.Abs(SidesAndDistance[i]) == 15 && objectNorth[k] == true)
                        {
                            if((Mathf.Abs(SidesAndDistance[0]) == 35))
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-5f + increment, 5f, 30f) + position, new Quaternion());
                            }
                            else
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-5f + increment, 5f, 10f) + position, new Quaternion());
                            }
                            wallTemp.transform.Rotate(0f, 90f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        else if (objectNorth[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(-25f + increment, 5f, 30f) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 90f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        increment += 10;
                        //objectNorth.Add(true);
                    }
                    else if (i == 2)
                    {
                        if (Mathf.Abs(SidesAndDistance[i]) == 15 && objectWest[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(-10f, 5f, -5f + increment) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 00f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        else if (objectWest[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(-10f, 5f, -25f + increment) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 00f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        increment += 10;
                        //objectWest.Add(true);
                    }
                    else if (i == 3)
                    {
                        
                        if (Mathf.Abs(SidesAndDistance[i]) == 15 && objectSouth[k] == true)
                        {
                            if ((Mathf.Abs(SidesAndDistance[0]) == 35))
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-5f + increment, 5f, -30f) + position, new Quaternion());
                            }
                            else
                            {
                                wallTemp = Instantiate(wallPrefab, new Vector3(-5f + increment, 5f, -10f) + position, new Quaternion());
                            }
                            wallTemp.transform.Rotate(0f, 90f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        else if (objectSouth[k] == true)
                        {
                            wallTemp = Instantiate(wallPrefab, new Vector3(-25f + increment, 5f, -30f) + position, new Quaternion());
                            wallTemp.transform.Rotate(0f, 90f, 90f);
                            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
                            Walls.Add(wallTemp);
                            wallTemp.transform.parent = gameObject.transform.parent;
                        }
                        increment += 10;
                        //objectSouth.Add(true);
                        
                    }
                }
            }
        }
    }

    void MakeRoom()
    {
        int PercentileChance = 90;
        GameObject tempSpawner;
        for (int i = 0; i < 3; i++)
        {
            if (Random.Range(0, 100) <= PercentileChance)
            {
                PercentileChance -= 20;
                if (gameObject.transform.rotation.eulerAngles.y % 180f != 0)
                {
                    tempSpawner = Instantiate(SpawnerPreFab, gameObject.transform.position + new Vector3(Random.Range(SizeOfRoom[0], SizeOfRoom[2]), .51f, Random.Range(SizeOfRoom[1], SizeOfRoom[3])), new Quaternion());

                }
                else
                {
                    tempSpawner = Instantiate(SpawnerPreFab, gameObject.transform.position + new Vector3(Random.Range(SizeOfRoom[1], SizeOfRoom[3]), .51f, Random.Range(SizeOfRoom[0], SizeOfRoom[2])), new Quaternion());
                }

                tempSpawner.transform.position = new Vector3(tempSpawner.transform.position.x, tempSpawner.transform.position.y + Terrain.activeTerrain.SampleHeight(tempSpawner.transform.position), tempSpawner.transform.position.z);

                tempSpawner.GetComponent<SpawnerControler>().WhatCreature = MonsterList[Random.Range(0, 3)];
                //tempSpawner.transform.parent = gameObject.transform;
                SpawnerList.Add(tempSpawner);
            }
        }
        //GameObject tempLogParent = Instantiate(new GameObject("ForestParent"), gameObject.transform);
        GameObject tempResourceGathering;
        PercentileChance = 40;
        for (int i = 0; i < 4; i++)
        {
            if(Random.Range(0,100) <= PercentileChance)
            {
                PercentileChance -= 10;
                if (gameObject.transform.rotation.eulerAngles.y % 180f != 0)
                {
                    tempResourceGathering = Instantiate(ReasourcePreFab, gameObject.transform.position + new Vector3(Random.Range(SizeOfRoom[0], SizeOfRoom[2]), 5.55f, Random.Range(SizeOfRoom[1], SizeOfRoom[3])), new Quaternion());
                }
                else
                {
                    tempResourceGathering = Instantiate(ReasourcePreFab, gameObject.transform.position + new Vector3(Random.Range(SizeOfRoom[1], SizeOfRoom[3]), 5.55f, Random.Range(SizeOfRoom[0], SizeOfRoom[2])), new Quaternion());
                }
                tempResourceGathering.transform.position = new Vector3(tempResourceGathering.transform.position.x, tempResourceGathering.transform.position.y + Terrain.activeTerrain.SampleHeight(tempResourceGathering.transform.position), tempResourceGathering.transform.position.z);
                //tempResourceGathering.transform.parent = gameObject.transform;
                ReasourceList.Add(tempResourceGathering);
            }
        }
        //SpawnGrass();
    }

    public void SpawnGrass()
    {
        int PercentileChance = 75;
        for (int i = 0; i < 60; i++)
        {
            if (Random.Range(0, 100) <= PercentileChance)
            {
                if (gameObject.transform.rotation.eulerAngles.y % 180f != 0)
                {
                    grassList.Add(Instantiate(RandomGeneration.instance.levelData.AllGrass[Random.Range(0, 7)], gameObject.transform.position + new Vector3(Random.Range(SizeOfRoom[0], SizeOfRoom[2]), .5f, Random.Range(SizeOfRoom[1], SizeOfRoom[3])), new Quaternion()));
                }
                else
                {
                    grassList.Add(Instantiate(RandomGeneration.instance.levelData.AllGrass[Random.Range(0, 7)], gameObject.transform.position + new Vector3(Random.Range(SizeOfRoom[1], SizeOfRoom[3]), .5f, Random.Range(SizeOfRoom[0], SizeOfRoom[2])), new Quaternion()));
                }

                //grassList.Add(Instantiate(RandomGeneration.instance.levelData.AllGrass[UnityEngine.Random.Range(0, 7)], gameObject.transform.position + new Vector3(Random.Range(-4.95f, 4.95f), .5f, Random.Range(-4.95f, 4.95f)), new Quaternion()));
                grassList[grassList.Count - 1].transform.Rotate(0f, Random.Range(0f, 180f), 0f);
                grassList[grassList.Count - 1].transform.parent = gameObject.transform;
            }
        }
        float OldRangeZ = (150f - (-150f));
        float OldRangeX = (400f - (-400f));
        float NewRange = (1f - 0f);

        for (int i = 0; i < grassList.Count; i++)
        {
            TreeInstance tempTree = new TreeInstance();
            //Debug.Log(System.Int32.Parse(grassList[i].name.Substring(grassList[i].name.Length - 9, 2)));
            tempTree.prototypeIndex = System.Int32.Parse(grassList[i].name.Substring(grassList[i].name.Length - 9, 2)) - 1;

            tempTree.color = Color.white;
            tempTree.lightmapColor = Color.white;

            tempTree.heightScale = 1;

            tempTree.widthScale = 1;

            float NewValueX = (((grassList[i].transform.position.x - (-400)) * NewRange) / OldRangeX) + 0;
            float NewValueZ = (((grassList[i].transform.position.z - (-150)) * NewRange) / OldRangeZ) + 0;

            tempTree.position = new Vector3(NewValueX, 0, NewValueZ);
            //Debug.Log(Terrain.activeTerrain.name);
            Terrain.activeTerrain.AddTreeInstance(tempTree);

            Terrain.activeTerrain.Flush();
        }
        foreach (GameObject grasstemp in grassList)
        {
            Destroy(grasstemp);
        }
        grassList.Clear();
    }

    public void SetParents()
    {
        foreach(GameObject tempSpawn in SpawnerList)
        {
            tempSpawn.transform.parent = gameObject.transform.parent;
        }
        foreach (GameObject tempResource in ReasourceList)
        {
            tempResource.transform.parent = gameObject.transform.parent;
        }
        DoDeleteOthers = false;
    }

    void RemoveOne(Collider other)
    {
        //Removes One
        if (other != null && other.tag == "Room")
        {
            if (other.gameObject.GetComponent<RoomGeneration>() != null)
            {
                if (!other.gameObject.GetComponent<RoomGeneration>().isDestroyed)
                {
                    isDestroyed = true;
                    Destroy(gameObject);
                    foreach(GameObject thing in ConnectingFloors)
                    {
                        Destroy(thing);
                    }
                }
            }
            else
            {
                if (!other.gameObject.transform.parent.gameObject.GetComponent<RoomGeneration>().isDestroyed)
                {
                    isDestroyed = true;
                    Destroy(gameObject);
                    foreach (GameObject thing in ConnectingFloors)
                    {
                        Destroy(thing);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isSafeRoom)
        {
            RemoveOne(other);
        }
    }

    void FixSize()
    {
        if (gameObject.GetComponent<BoxCollider>() != null)
        {
            gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
        else
        {
            foreach (BoxCollider tempbox in gameObject.GetComponentsInChildren<BoxCollider>())
            {
                tempbox.isTrigger = false;
            }
        }
        gameObject.transform.localScale += new Vector3(.1f, 0, .1f);
        gameObject.transform.position = tempPost;
    }

    private void OnDestroy()
    {
        RandomGeneration.instance.allRooms.Remove(this.gameObject);
        if (DoDeleteOthers)
        {
            foreach (GameObject tempSpawn in SpawnerList)
            {
                Destroy(tempSpawn);
            }
            foreach (GameObject tempResource in ReasourceList)
            {
                Destroy(tempResource);
            }
            foreach (GameObject tempWalls in Walls)
            {
                Destroy(tempWalls);
            }
            foreach (GameObject tempfloor in ConnectingFloors)
            {
                RandomGeneration.instance.allFloors.Remove(tempfloor);
                Destroy(tempfloor);
            }
            foreach (GameObject grassTemp in grassList)
            {
                Destroy(grassTemp);
            }
        }
    }
}
