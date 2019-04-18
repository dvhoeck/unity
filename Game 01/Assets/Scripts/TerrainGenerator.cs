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

    // Start is called before the first frame update
    private void Start()
    {
        OffsetX = UnityEngine.Random.Range(0f, 9999f);
        OffsetY = UnityEngine.Random.Range(0f, 9999f);
    }

    // Update is called once per frame
    private void Update()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);

        // offsetting X simulates terrain coming towards the player, this give the impression of forward movement
        OffsetX += Time.deltaTime * TerrainSpeed;
    }

    /// <summary>
    /// Generate a
    /// </summary>
    /// <param name="terrainData"></param>
    /// <returns></returns>
    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        // generate a height map
        terrainData.heightmapResolution = Width + 1;
        terrainData.size = new Vector3(Width, Depth, Height);
        terrainData.SetHeights(0, 0, GenerateHeights());

        _counter += Time.deltaTime * TerrainSpeed * 10f;

        // move the texture at the same speed as the height map is offset
        terrainData.terrainLayers[0].tileOffset = new Vector2(0.0f, _counter);

        return terrainData;
    }

    /// <summary>
    /// Create different heights in a coordinate system.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private float[,] GenerateHeights()
    {
        var heights = new float[Width, Height];

        for (var x = 0; x < Width; x++)
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