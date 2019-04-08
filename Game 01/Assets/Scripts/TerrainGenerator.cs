using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int Depth = 2;
    public int Width = 256;
    public int Height = 256;

    public float Scale = 20f;

    public float OffsetX = 100f;
    public float OffsetY = 100f;

    public float TerrainSpeed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        OffsetX = UnityEngine.Random.Range(0f, 9999f);
        OffsetY = UnityEngine.Random.Range(0f, 9999f);


    }

    // Update is called once per frame
    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        OffsetX += Time.deltaTime * TerrainSpeed;
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = Width + 1;
        terrainData.size = new Vector3(Width, Depth, Height);
        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        var heights = new float[Width, Height];

        for(var x = 0; x < Width; x++)
        {
            for (var y = 0; y < Height; y++)
            {
                heights[x, y] = CalculateHeight(x, y);
            }

        }

        return heights;
    }

    private float CalculateHeight(int x, int y)
    {
        var xCoord = (float)x / Width * Scale + OffsetX;
        var yCoord = (float)y / Height * Scale + OffsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
