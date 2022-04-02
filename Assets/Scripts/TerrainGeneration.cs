using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public float depth = 20;
    public float width = 256f; //800
    public float height = 256f; //300

    public float scale = 20f;

    private void start()
    {
        Terrain terrian = GetComponent<Terrain>();
        terrian.terrainData = GenerateTerrian(terrian.terrainData);
    }

    TerrainData GenerateTerrian(TerrainData terrainData)
    {
        terrainData.heightmapResolution = (int)width + 1;

        //terrainData.size = new Vector3(width, depth, height);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        //Debug.Log(width);
        float[,] heights = new float[(int)width, (int)height];
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }
        }
        return heights;
    }

    float CalculateHeight (int x, int y)
    {
        float XCoord = (float) x / width * scale;
        float YCoord = (float) y / height * scale;

        return Mathf.PerlinNoise(XCoord, YCoord);
    }
}
