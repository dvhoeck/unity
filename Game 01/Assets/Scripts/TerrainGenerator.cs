using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public int Depth = 2;
    public int Width = 256;
    public int Height = 256;
    public bool DoTerrainOffset;

    public float Scale = 20f;

    public float OffsetX = 100f;
    public float OffsetY = 100f;

    public float TerrainSpeed = 15.0f;

    private float _counter;

    private SplatPrototype[] splatPrototypes;

    // Start is called before the first frame update
    void Start()
    {
        OffsetX = UnityEngine.Random.Range(0f, 9999f);
        OffsetY = UnityEngine.Random.Range(0f, 9999f);

        //splatPrototypes = 
    }

    // Update is called once per frame
    void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        /*
        _counter += 0.1f;

        var tmp = terrain.terrainData.terrainLayers[0];
        tmp.tileOffset = new Vector2(0.0f, _counter);

        var tmp2 = new TerrainLayer
        {
            diffuseTexture = null,// tmp.diffuseTexture,
            tileOffset = new Vector2(0.0f, _counter)
        };

        terrain.terrainData.terrainLayers[0] = tmp2;
        */
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        OffsetX += Time.deltaTime * TerrainSpeed;
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = Width + 1;
        terrainData.size = new Vector3(Width, Depth, Height);
        terrainData.SetHeights(0, 0, GenerateHeights());


        _counter += Time.deltaTime * TerrainSpeed * 10f;
        /*
        var tmp = terrainData.terrainLayers[0];
        tmp.tileOffset = new Vector2(0.0f, _counter);

        var tmp2 = new TerrainLayer
        {
            diffuseTexture = null,// tmp.diffuseTexture,
            tileOffset = new Vector2(0.0f, _counter)
        };*/

        terrainData.terrainLayers[0].tileOffset = new Vector2(0.0f, _counter);

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
        float xCoord, yCoord;

        if (DoTerrainOffset)
        {
            xCoord = (float)x / Width * Scale + OffsetX;
            yCoord = (float)y / Height * Scale + OffsetY;
        }
        else
        {
            xCoord = (float)x / Width * Scale;
            yCoord = (float)y / Height * Scale;
        }
        
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
