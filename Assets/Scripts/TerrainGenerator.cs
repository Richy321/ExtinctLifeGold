using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGenerator : MonoBehaviour
{
    public GameObject OceanTile;
    public GameObject WaterTile;
    public GameObject SandTile;
    public GameObject GrassTile;
    public GameObject PlainTile;
    public GameObject MountainTile;

    public GameObject confirmButton;

    public Vector2 gridSize;

    public static string terrainGridGOName = "TerrainGrid";

    const float oceanThreshold = 0.0f;
    const float waterThreshold = 0.2f;
    const float sandThreshold = 0.4f;
    const float grassThreshold = 0.6f;
    const float mountainThreshold = 0.8f;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Generate()
    {
        //using Fractal Brownian Motion
        float gain = 0.65f;
        float lacunarity = 2.0f;
        int octaves = 16;
        List<float> heightMap = new List<float>();
        float min = float.MaxValue, max = float.MinValue;
        Random.seed = (int)System.DateTime.Now.Ticks;

        int seed = Random.Range(0, (int)gridSize.x);

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                float total = 0.0f;
			    float frequency = 1.25f / (float)(gridSize.x+1);
			    float amplitude = gain;
                
                for (int i = 0; i < octaves; ++i)
                {
                    total += Mathf.PerlinNoise((float)(x + seed) * frequency, (float)(y + seed) * frequency) * amplitude;
                    frequency *= lacunarity;
                    amplitude *= gain;
                }
                min = Mathf.Min(min, total);
                max = Mathf.Max(max, total);

                heightMap.Add(total);
            }
        }

        //normalisation
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                float value = heightMap[(int)(x * gridSize.y + y)];

                heightMap[getGridCoord(x,y)] = (value - min) / (max - min);
            }
        }

        GenerateTilesFromHeightMap(heightMap);

        confirmButton.SetActive(true);
    }

    void GenerateTilesFromHeightMap(List<float> heightMap)
    {
        //generate at origin
        SpriteRenderer spriteRenderer = OceanTile.GetComponent<SpriteRenderer>();
        Vector3 tileSize = spriteRenderer.sprite.bounds.size;

        GameObject terrainGridGO = GameObject.Find(terrainGridGOName);

        Vector2 centeredOffset = new Vector2(-tileSize.x * (gridSize.x * 0.5f), -tileSize.y * (gridSize.y * 0.5f));

        if (terrainGridGO)
        {
            GameObject.DestroyObject(terrainGridGO);
        }

        terrainGridGO = new GameObject(terrainGridGOName);
        TerrainManager manager = terrainGridGO.AddComponent<TerrainManager>();
        manager.gridSize = gridSize;

        GameObject newTile;

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                float heightValue = heightMap[getGridCoord(x, y)];

                if (heightValue > mountainThreshold)
                    newTile = GameObject.Instantiate(MountainTile);
                else if(heightValue > grassThreshold)
                    newTile = GameObject.Instantiate(GrassTile);
                else if(heightValue > sandThreshold)
                    newTile = GameObject.Instantiate(SandTile);
                else if (heightValue > waterThreshold)
                    newTile = GameObject.Instantiate(WaterTile);
                else //(heightValue > oceanThreshold)
                    newTile = GameObject.Instantiate(OceanTile);

                Tile tileScript = newTile.GetComponent<Tile>();
                newTile.name = "Tile: " + x + "," + y;
                newTile.transform.parent = terrainGridGO.transform;
                newTile.transform.Translate(centeredOffset.x, centeredOffset.y, 0); //offset to generate centered
                newTile.transform.Translate(x * tileSize.x, y * tileSize.y, 0);
                tileScript.xCoord = x;
                tileScript.yCoord = y;
                manager.AddTile(tileScript);
            }
        }
        manager.CalculateBattlegrounds();
    }

    public int getGridCoord(int x, int y)
    {
        return (int)(x * gridSize.y + y);
    }
}
