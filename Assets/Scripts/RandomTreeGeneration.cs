using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTreeGeneration : MonoBehaviour
{
    public List<GameObject> Tree = new List<GameObject>();
    public Terrain terrain;
    private int terrainWidth;
    private int terrainLength;
    private int terrainPosX;
    private int terrainPosZ;
    void Start()
    {
        terrainWidth = (int)terrain.terrainData.size.x;
        terrainLength = (int)terrain.terrainData.size.z;
        terrainPosX = (int)terrain.transform.position.x;
        terrainPosZ = (int)terrain.transform.position.z;
        System.Array.Clear(terrain.terrainData.treeInstances, 0, terrain.terrainData.treeInstances.Length);
        terrain.terrainData.treeInstances = new TreeInstance[0];
        StartCoroutine(SpawnTrees());
    }

    IEnumerator SpawnTrees()
    {
        int amountOfTrees = 1500;
        for (int i = 0; i < amountOfTrees; i++)
        {
            //yield return new WaitForSeconds(.0001f);
            int posX = Random.Range(terrainPosX, terrainPosX + terrainWidth);
            int posZ = Random.Range(terrainPosZ, terrainPosZ + terrainLength);
            float posY = Terrain.activeTerrain.SampleHeight(new Vector3(posX, 0, posZ));
            GameObject newTree = Instantiate(Tree[Random.Range(0, Tree.Count)], new Vector3(posX, posY, posZ), Quaternion.identity);
            newTree.transform.parent = terrain.transform;
        }
        yield return new WaitForSeconds(2f);
        float OldRangeZ = (150f - (-150f));
        float OldRangeX = (400f - (-400f));
        float NewRange = (1f - 0f);

        for (int i = 0; i < terrain.transform.childCount; i++)
        {
            Destroy(terrain.transform.GetChild(i).GetComponent<BoxCollider>());
            Destroy(terrain.transform.GetChild(i).GetComponent<DeleteTree>());

            TreeInstance tempTree = new TreeInstance();
            //Debug.Log(terrain.transform.GetChild(i).name.Substring(terrain.transform.GetChild(i).name.Length - 8, 1));
            tempTree.prototypeIndex = System.Int32.Parse(terrain.transform.GetChild(i).name.Substring(terrain.transform.GetChild(i).name.Length - 9, 2)) - 1;

            tempTree.color = Color.white;
            tempTree.lightmapColor = Color.white;

            tempTree.heightScale = 1;

            tempTree.widthScale = 1;
            //Debug.Log(terrain.transform.GetChild(i).position);
            float NewValueX = (((terrain.transform.GetChild(i).position.x - (-400)) * NewRange) / OldRangeX) + 0;
            float NewValueZ = (((terrain.transform.GetChild(i).position.z - (-150)) * NewRange) / OldRangeZ) + 0;
            //Debug.Log(NewValueX);
            //Debug.Log(NewValueZ);

            tempTree.position = new Vector3(NewValueX, 0, NewValueZ);

            terrain.AddTreeInstance(tempTree);

            terrain.Flush();
        }

        foreach (Transform child in terrain.transform)
        {
            Destroy(child.gameObject);
        }
        //terrain.Flush();
        //yield return new WaitForSecondsRealtime(.5f);



        StopCoroutine(SpawnTrees());
        //Debug.Log("is it here");
        this.gameObject.GetComponent<RandomTreeGeneration>().enabled = false;
        yield return null;
    }

}
