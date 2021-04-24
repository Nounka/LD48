using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    
    public int width;
    public int length;
    public Tilemap tilemap;

    public TileBase grassTile;
    public TileBase dirtTile;
    public TileBase waterTile;

    public Tile[] tiles;

    public List<ElementSpawnParams> elementSpawnParams;

    public PerlinNoise forestDensityPerlin;
    public PerlinNoise humidityPerlin;

    void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();

        forestDensityPerlin = new PerlinNoise();
        forestDensityPerlin.Init(20, 20);

        humidityPerlin = new PerlinNoise();
        humidityPerlin.Init(20, 20);

        GenerateTiles();
        DrawTileMap();
        GenerateElements();
    }

    void Update()
    {

    }

    void GenerateTiles()
    {
        tiles = new Tile[width * length];


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {

                float humidity = (forestDensityPerlin.Sample(x, y) + 1) / 2;
                float forestDensity = ((y / length) + forestDensityPerlin.Sample(x, y) * 0.2f + (humidity - 0.5f) * 0.3f);

                Tile tile = new Tile();
                tile.humidity = humidity;
                tile.density = forestDensity;

                tile.isWater = false;

                if (humidity > 0.9)
                {
                    tile.texture = waterTile;
                }
                else if(humidity > 0.3)
                {
                    tile.texture = grassTile;
                }
                else
                {
                    tile.texture = dirtTile;
                }
                tiles[y * width + x] = tile;
            }
        }
    }

    void GenerateElements()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                float randomValue = Random.value;
                float randomCount = 0.0f;
                foreach (ElementSpawnParams param in elementSpawnParams)
                {
                    float proba = ((length - y) * param.spawnProbabilityEdge + y * param.spawnProbabilityCenter) / length;
                    if (randomCount + proba < randomValue)
                    {
                        GameObject element = Instantiate(param.elementPrefab);
                        WorldStaticObject elementStatic = element.GetComponent<WorldStaticObject>();
                        if (elementStatic)
                        {
                            bool canSpawn = true;
                            for (int i = x; i < x + elementStatic.width; i++)
                            {
                                for (int j = y; j < y + elementStatic.length; j++)
                                {
                                    if (x < width && j < length)
                                    {
                                        Tile tile = GetTile(i, j);
                                        if(tile.relatedObject != null)
                                        {
                                            canSpawn = false;
                                        }
                                    }
                                    else
                                    {
                                        canSpawn = false;
                                    }
                                }
                            }
                            if (canSpawn)
                            {
                                for (int i = x; i < x + elementStatic.width; i++)
                                {
                                    for (int j = y; j < y + elementStatic.length; j++)
                                    {
                                        if (x < width && j < length)
                                        {
                                            Tile tile = GetTile(i, j);
                                            tile.relatedObject = elementStatic;
                                        }
                                    }
                                }
                                element.transform.position = new Vector3(x, y, 0);
                                break;
                            }
                            else
                            {
                                Destroy(element);
                                randomCount += proba;
                            }
                        }
                    }
                    else
                    {
                        randomCount += proba;
                    }
                }
            }
        }
    }

    void DrawTileMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), GetTile(x, y).texture);
            }
        }
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[y * width + x];
    }
}

public class Tile
{
    public TileBase texture;
    public bool isWater;
    public WorldStaticObject relatedObject;

    public float humidity;
    public float density;
}
