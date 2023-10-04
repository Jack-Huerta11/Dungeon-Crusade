using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int width = 256; // Width of the terrain
    public int length = 256; // Length of the terrain
    public float scale = 20.0f; // Scale of the terrain (adjust to control terrain roughness)
    public float heightMultiplier = 10.0f; // Adjust to control terrain height
    public int seed = 0; // Seed for random terrain generation

    public int minHills = 1; // Minimum number of hills
    public int minCaves = 1; // Minimum number of caves

    public int maxHills = 5; // Maximum number of hills
    public int maxCaves = 5; // Maximum number of caves

    public float hillHeightScale = 5.0f; // Height scale for hills
    public float caveHeightScale = -5.0f; // Height scale for caves

    void Start()
    {
        if (seed == 0)
            seed = Random.Range(1, 100000);

        GenerateTerrain();
    }

    void GenerateTerrain()
    {
        Terrain terrain = GetComponent<Terrain>();

        Random.InitState(seed);

        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, heightMultiplier, length);
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        float[,] heights = new float[width, length];

        int numHills = Random.Range(minHills, maxHills + 1);
        int numCaves = Random.Range(minCaves, maxCaves + 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                float hillHeight = GenerateHills(x, y, numHills);
                float caveHeight = GenerateCaves(x, y, numCaves);

                // Combine hill and cave heights
                float finalHeight = hillHeight + caveHeight;

                heights[x, y] = finalHeight;
            }
        }

        return heights;
    }

    float GenerateHills(int x, int y, int numHills)
    {
        float hillHeight = 0;

        for (int i = 0; i < numHills; i++)
        {
            float xCoord = (float)x / width * scale * Random.Range(0.5f, 2.0f);
            float yCoord = (float)y / length * scale * Random.Range(0.5f, 2.0f);

            hillHeight += Mathf.PerlinNoise(xCoord, yCoord);
        }

        return hillHeight * hillHeightScale;
    }

    float GenerateCaves(int x, int y, int numCaves)
    {
        float caveHeight = 0;

        for (int i = 0; i < numCaves; i++)
        {
            float xCoord = (float)x / width * scale * Random.Range(0.5f, 2.0f);
            float yCoord = (float)y / length * scale * Random.Range(0.5f, 2.0f);

            caveHeight += Mathf.PerlinNoise(xCoord, yCoord);
        }

        return caveHeight * caveHeightScale;
    }
}
