using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FloorGeneration : MonoBehaviour
{
    public GameObject GrassPreFab;

    public GameObject torchObject;

    public bool isDestroyed = false;
    public GameObject MainFloorExtra;
    public GameObject RoomICameFrom;
    public List<GameObject> otherFloors = new List<GameObject>();

    public bool FirstFloor = false;
    public bool CanMakeNewFloor = false;

    public int WhatDirection = -1;

    List<GameObject> Walls = new List<GameObject>();
    public GameObject wallPrefab;

    List<GameObject> grassList = new List<GameObject>();

    bool objectNorth = true, objectSouth = true, objectEast = true, objectWest = true;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            if (gameObject.GetComponent<BoxCollider>() != null)
            {
                gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
            else
            {
                foreach (BoxCollider tempbox in gameObject.GetComponentsInChildren<BoxCollider>())
                {
                    if (tempbox != null)
                    {
                        tempbox.isTrigger = true;
                    }
                }
            }
            if (CanMakeNewFloor)
            {
                Invoke("GenerateFloor", .00001f);
            }
            Invoke("FixSize", .15f);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
        }
    }
    public void GenerateTorch()
    {
        if (UnityEngine.Random.Range(0f, 60f) < 3f)
        {
            GameObject tempTorch = Instantiate(torchObject, transform.position, torchObject.transform.rotation);
            Vector3 tempPos1 = tempTorch.transform.position;
            tempPos1.y = Terrain.activeTerrain.SampleHeight(tempTorch.transform.position) + 1.3f;
            tempPos1.x = tempTorch.transform.position.x + UnityEngine.Random.Range(-5f, 5f);
            tempPos1.z = tempTorch.transform.position.z + UnityEngine.Random.Range(-5f, 5f);
            tempTorch.transform.position = tempPos1;
        }
    }


    public void GenerateWalls()
    {
        //bool objectNorth = true, objectSouth = true, objectEast = true, objectWest = true;

        List<GameObject> taggedObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hallway_Norm"));
        taggedObjects.AddRange(GameObject.FindGameObjectsWithTag("Hallway"));
        float distance = 10;
        Vector3 position = transform.position;
        foreach (GameObject temp in taggedObjects)
        {
            Vector3 diff = temp.transform.position - position;
            float diffz = Mathf.Abs(temp.transform.position.z - position.z);
            float diffx = Mathf.Abs(temp.transform.position.x - position.x);
            //float curDistance = diff.sqrMagnitude;

            if (temp != gameObject && !FirstFloor)
            {
                if (temp.transform.position.x == position.x && temp.transform.position.z > position.z && diffz <= distance)
                {
                    objectNorth = false;

                }
                if (temp.transform.position.z == position.z && temp.transform.position.x > position.x && diffx <= distance)
                {
                    objectEast = false;

                }
                if (temp.transform.position.x == position.x && temp.transform.position.z < position.z && diffz <= distance)
                {
                    objectSouth = false;

                }
                if (temp.transform.position.z == position.z && temp.transform.position.x < position.x && diffx <= distance)
                {
                    objectWest = false;
                }
            }
        }
        taggedObjects.Clear();
        taggedObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Room"));
        int distancex, distancez;

        foreach (GameObject temp in taggedObjects)
        {
            Vector3 diff = temp.transform.position - position;
            float diffz = Mathf.Abs(temp.transform.position.z - position.z);
            float diffx = Mathf.Abs(temp.transform.position.x - position.x);
            if (temp.transform.rotation.y - 90 == 0 || temp.transform.rotation.y + 90 == 0)
            {
                distancex = (int)temp.GetComponent<RoomGeneration>().SidesAndDistance[1];
                distancez = (int)temp.GetComponent<RoomGeneration>().SidesAndDistance[0];
            }
            else
            {
                distancex = (int)temp.GetComponent<RoomGeneration>().SidesAndDistance[0]; //if its wierd its prop this
                distancez = (int)temp.GetComponent<RoomGeneration>().SidesAndDistance[1];
            }
            /*
            if ((temp.transform.position.x == (position.x + 5) || temp.transform.position.x == (position.x - 5)) && temp.transform.position.z > position.z && diffz < distancez)
            {
                objectNorth = false;
            }
            if ((temp.transform.position.z == (position.z) || temp.transform.position.z == (position.z)) && temp.transform.position.x > position.x && diffx < distancex)
            {
                objectEast = false;
            }
            if ((temp.transform.position.x == (position.x + 5) || temp.transform.position.x == (position.x - 5)) && temp.transform.position.z < position.z && diffz < distancez)
            {
                objectSouth = false;
            }
            if ((temp.transform.position.z == (position.z) || temp.transform.position.z == (position.z)) && temp.transform.position.x < position.x && diffx < distancex)
            {
                objectWest = false;
            }
            */
        }

        GameObject wallTemp;
        if (objectNorth)
        {
            wallTemp = Instantiate(wallPrefab, new Vector3(0f, 5f, 5f) + position, new Quaternion());
            wallTemp.transform.Rotate(0f, 90f, 90f);
            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
            Walls.Add(wallTemp);
            wallTemp.transform.parent = gameObject.transform;
        }
        if (objectEast)
        {
            wallTemp = Instantiate(wallPrefab, new Vector3(5f, 5f, 0f) + position, new Quaternion());
            wallTemp.transform.Rotate(0f, 00f, 90f);
            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
            Walls.Add(wallTemp);
            wallTemp.transform.parent = gameObject.transform;
        }
        if (objectSouth)
        {
            wallTemp = Instantiate(wallPrefab, new Vector3(0f, 5f, -5f) + position, new Quaternion());
            wallTemp.transform.Rotate(0f, 90f, 90f);
            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
            Walls.Add(wallTemp);
            wallTemp.transform.parent = gameObject.transform;
        }
        if (objectWest)
        {
            wallTemp = Instantiate(wallPrefab, new Vector3(-5f, 5f, 0f) + position, new Quaternion());
            wallTemp.transform.Rotate(0f, 00f, 90f);
            wallTemp.GetComponent<WallChecker>().WhatSpawnedMe = gameObject;
            Walls.Add(wallTemp);
            wallTemp.transform.parent = gameObject.transform;
        }
    }

    void GenerateFloor()
    {
        GameObject newFloor = null;

        List<GameObject>taggedObjects = new List<GameObject>(GameObject.FindGameObjectsWithTag("Hallway"));
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        //This is for cloest in any direction.
        /*
        foreach (GameObject temp in taggedObjects)
        {
            Vector3 diff = temp.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && !otherFloors.Contains(temp) && temp != gameObject)
            {
                closest = temp;
                distance = curDistance;
            }
        }
        */
        foreach (GameObject temp in taggedObjects)
        {
            Vector3 diff = temp.transform.position - position;
            float diffz = Mathf.Abs(temp.transform.position.z - position.z);
            float diffx = Mathf.Abs(temp.transform.position.x - position.x);
            float curDistance = diff.sqrMagnitude;
            if (!otherFloors.Contains(temp) && temp != gameObject)
            {
                //Debug.Log(WhatDirection);
                if (WhatDirection == 0 && temp.transform.position.z > position.z && diffz < distance)
                {
                    //Debug.Log("Does this ever happen 1");
                    closest = temp;
                    distance = diffz;
                }
                else if (WhatDirection == 1 && temp.transform.position.x > position.x && diffx < distance)
                {
                    //Debug.Log("Does this ever happen 2");
                    closest = temp;
                    distance = diffx;
                }
                else if (WhatDirection == 2 && temp.transform.position.z < position.z && diffz < distance)
                {
                    closest = temp;
                    distance = diffz;                                                  
                }
                else if (WhatDirection == 3 && temp.transform.position.x < position.x && diffx < distance)
                {
                    closest = temp;
                    distance = diffx;
                }
                else if (WhatDirection == -1 && curDistance < distance)
                {
                    Debug.Log("This Should never happen");
                    closest = temp;
                    distance = curDistance;
                }
            }
        }
        //makes the new floor where it suppose to go based of what floor space is closets
        if (closest != null && !FirstFloor)
        {  
            if(Mathf.Abs(position.x - closest.transform.position.x) > Mathf.Abs(position.z - closest.transform.position.z))
            {
                if (position.x - closest.transform.position.x > 0) //postive
                {
                    newFloor = Instantiate(MainFloorExtra, new Vector3(position.x - 10, position.y, position.z), new Quaternion());
                }
                else //negative
                {
                    newFloor = Instantiate(MainFloorExtra, new Vector3(position.x + 10, position.y, position.z), new Quaternion());
                }
            }
            else
            {
                if (0 < position.z - closest.transform.position.z) //postive
                {
                    newFloor = Instantiate(MainFloorExtra, new Vector3(position.x, position.y, position.z - 10), new Quaternion());
                }
                else //negative
                {
                    newFloor = Instantiate(MainFloorExtra, new Vector3(position.x, position.y, position.z + 10), new Quaternion());
                }
                
            }
            
        }
        else if(closest != null && FirstFloor)
        {
            if (WhatDirection == 0) //Nortth
            {
                newFloor = Instantiate(MainFloorExtra, new Vector3(position.x, position.y, position.z + 10), new Quaternion());
            }
            else if (WhatDirection == 2) //South
            {
                newFloor = Instantiate(MainFloorExtra, new Vector3(position.x, position.y, position.z - 10), new Quaternion());
            }
            else if (WhatDirection == 1) //East
            {
                newFloor = Instantiate(MainFloorExtra, new Vector3(position.x + 10, position.y, position.z), new Quaternion());
            }
            else if (WhatDirection == 3) //West
            {
                newFloor = Instantiate(MainFloorExtra, new Vector3(position.x - 10, position.y, position.z), new Quaternion());
            }
        }
        else
        {
            if(FirstFloor || (GameObject.FindGameObjectWithTag("Hallway").transform.position - position).sqrMagnitude <= 1100)
            {
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
        if(newFloor!=null)
        {
            newFloor.GetComponent<FloorGeneration>().RoomICameFrom = RoomICameFrom;
            newFloor.GetComponent<FloorGeneration>().otherFloors = otherFloors;
            newFloor.GetComponent<FloorGeneration>().WhatDirection = WhatDirection;
            RandomGeneration.instance.allFloors.Add(newFloor);
        }
        //SpawnGrass();
    }



    public void SpawnGrass()
    {
        int PercentileChance = 50;
        for (int i = 0; i < 10; i++)
        {
            if (UnityEngine.Random.Range(0, 100) <= PercentileChance)
            {
                grassList.Add(Instantiate(RandomGeneration.instance.levelData.AllGrass[UnityEngine.Random.Range(0, 7)], gameObject.transform.position + new Vector3(UnityEngine.Random.Range(-4.95f, 4.95f), .5f, UnityEngine.Random.Range(-4.95f, 4.95f)), new Quaternion()));
                grassList[grassList.Count - 1].transform.Rotate(0f, UnityEngine.Random.Range(0f, 180f), 0f);
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
            tempTree.prototypeIndex = System.Int32.Parse(grassList[i].name.Substring(grassList[i].name.Length - 9, 2) ) - 1;

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
        foreach(GameObject grasstemp in grassList)
        {
            Destroy(grasstemp);
        }
        grassList.Clear();
    }

    void RemoveOne(Collider other)
    {
        //RandomGeneration.instance.allFloors.Remove(this.gameObject);
        //Removes One
        if (other != null && other.tag == "Hallway_Norm" && gameObject.tag != "Hallway")
        {
            if (other.gameObject.GetComponent<FloorGeneration>() != null)
            {
                if (!other.gameObject.GetComponent<FloorGeneration>().isDestroyed)
                {
                    RandomGeneration.instance.allFloors.Remove(this.gameObject);
                    isDestroyed = true;
                    Destroy(gameObject);
                }
            }
            else
            {
                if (!other.gameObject.transform.parent.gameObject.GetComponent<FloorGeneration>().isDestroyed)
                {
                    RandomGeneration.instance.allFloors.Remove(this.gameObject);
                    isDestroyed = true;
                    Destroy(gameObject);
                }
            }
        }
        else if (other != null && other.tag == "Room")
        {
            if (other.gameObject.GetComponent<RoomGeneration>() != null)
            {
                RandomGeneration.instance.allFloors.Remove(this.gameObject);
                isDestroyed = true;
                Destroy(gameObject);
            }
            else
            {
                RandomGeneration.instance.allFloors.Remove(this.gameObject);
                isDestroyed = true;
                Destroy(gameObject);
            }
        }
        else if (other != null && other.tag == "Hallway" && gameObject.tag != "Hallway")
        {
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RemoveOne(other);
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
    }

    void OnDestroy()
    {
        RandomGeneration.instance.allFloors.Remove(gameObject);
        foreach (GameObject grassTemp in grassList)
        {
            Destroy(grassTemp);
        }
        grassList.Clear();
    }
}
