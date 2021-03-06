using System.Collections;
using System.Collections.Generic;

using System;
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
    public List<RiverDefinition> rivers;

    public PerlinNoise forestDensityPerlin;
    public PerlinNoise humidityPerlin;
    public PerlinNoise humidityMacroPerlin;

    void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();

        forestDensityPerlin = new PerlinNoise();
        forestDensityPerlin.Init(20, 20);

        humidityPerlin = new PerlinNoise();
        humidityPerlin.Init(20, 20);

        humidityMacroPerlin = new PerlinNoise();
        humidityMacroPerlin.Init(30, 30);

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

                float humidity = (humidityPerlin.Sample(x * 1.0f / 400, y * 1.0f / 400) + 1) / 4 + (humidityMacroPerlin.Sample(x * 1.0f / 1000, y * 1.0f / 1000) + 1) / 4;
                foreach(RiverDefinition river in rivers)
                {
                    int centerYAtXPos = ((width - x) * river.posYLeft + x * river.posYRight) / width + Mathf.RoundToInt(3 * Mathf.Sin(x / 3*width));
                    int distanceFromRiverCenter = Mathf.Abs(centerYAtXPos - y);
                    if(distanceFromRiverCenter < river.width)
                    {
                        humidity = 1.0f;
                    }
                    else if(distanceFromRiverCenter < river.width + river.widthBorder)
                    {
                        humidity = 0.6f;
                    }
                }
                float forestDensity = ((y / length) + forestDensityPerlin.Sample(x * 1.0f / 200, y * 1.0f / 200) * 0.2f + (humidity - 0.5f) * 0.3f);

                Tile tile = new Tile();
                tile.position = new Vector2Int(x, y);
                tile.humidity = humidity;
                tile.density = forestDensity;

                tile.isWater = false;

                if (humidity > 0.7)
                {
                    tile.texture = waterTile;
                    tile.isWater = true;
                    tile.isBlocking = true;
                }
                else if(humidity > 0.4)
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

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Tile tile = tiles[y * width + x];
                tile.neighbours = new List<Tile>();
                if(x > 0)
                {
                    tile.neighbours.Add(tiles[y * width + x - 1]);
                }
                if(x < width - 1)
                {
                    tile.neighbours.Add(tiles[y * width + x + 1]);
                }
                if(y > 0)
                {
                    tile.neighbours.Add(tiles[(y - 1) * width + x]);
                }
                if(y < length - 1)
                {
                    tile.neighbours.Add(tiles[(y + 1) * width + x]);
                }
            }
        }
    }

    void GenerateElements()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                float randomValue = UnityEngine.Random.value;
                float randomCount = 0.0f;
                foreach (ElementSpawnParams param in elementSpawnParams)
                {
                    float proba = ((length - y) * param.spawnProbabilityEdge + y * param.spawnProbabilityCenter) / length;
                    if (randomCount + proba > randomValue)
                    {
                        GameObject element = Instantiate(param.elementPrefab);
                        WorldStaticObject elementStatic = element.GetComponent<WorldStaticObject>();
                        if (elementStatic)
                        {
                            bool canSpawn = true;
                            for (int i = x; i < x + elementStatic.size.x; i++)
                            {
                                for (int j = y; j < y + elementStatic.size.y; j++)
                                {
                                    if (x < width && j < length)
                                    {
                                        Tile tile = GetTile(i, j);
                                        if(tile.relatedObject != null || tile.isWater)
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
                                for (int i = x; i < x + elementStatic.size.x; i++)
                                {
                                    for (int j = y; j < y + elementStatic.size.y; j++)
                                    {
                                        if (x < width && j < length)
                                        {
                                            Tile tile = GetTile(i, j);
                                            tile.relatedObject = elementStatic;
                                            if (elementStatic.isBlocking)
                                            {
                                                tile.isBlocking = true;
                                            }
                                            
                                        }
                                    }
                                }
                                elementStatic.position = new Vector2Int(x, y);
                                element.transform.position = new Vector3(x + 0.5f, y, 0);
                                break;
                            }
                            else
                            {
                                Destroy(element);
                                randomCount += proba;
                            }
                        }
                        else
                        {
                            Sprite sprite = element.GetComponent<SpriteRenderer>().sprite;
                            element.transform.position = new Vector3(x + 0.5f, y, 0);
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

    public Tile GetTile(Vector3 pos)
    {
        return GetTile(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }

    public Path GetPath(Tile origin, Tile destination)
    {
        // Can't compute path to a non reachable object
        if (destination.isBlocking) {
            return null;
        }

        bool[] wasSeen = new bool[width * length];

        Waypoint current = new Waypoint();
        current.relatedTile = origin;
        current.Cost = 0;
        current.estimatedCost = EstimateDistance(origin, destination);

        List<Waypoint> open = new List<Waypoint>();
        open.Add(current);

        bool found = false;
        int count = open.Count;
        while(!found && count > 0)
        {
            Waypoint bestPoint = open[0];
            int bestIndex = 0;
            for (int index = 0; index < count; index++)
            {
                Waypoint wp = open[index];
                if (wp.estimatedCost < bestPoint.estimatedCost)
                {
                    bestPoint = wp;
                    bestIndex = index;
                }
            }

            Tile bestPointTile = bestPoint.relatedTile;
            open.RemoveAt(bestIndex);
            
            foreach(Tile tile in bestPointTile.neighbours)
            {
                // We don't handle already visited spaces, nor blocking tiles
                if (tile.isBlocking || wasSeen[tile.position.y * width + tile.position.x]) {
                    continue;
                }
                if(tile == destination)
                {
                    Path path = new Path();
                    path.waypoints = new List<Waypoint>();
                    Waypoint wp = new Waypoint();
                    wp.relatedTile = destination;
                    wp.Cost = bestPoint.Cost + 1;
                    wp.estimatedCost = wp.Cost;
                    wp.origin = bestPoint;
                    path.waypoints.Add(wp);
                    while (wp.origin != null && wp.origin.relatedTile != origin)
                    {
                        path.waypoints.Add(wp.origin);
                        wp = wp.origin;
                    }
                    return path;
                }
                Waypoint w = new Waypoint();
                w.relatedTile = tile;
                w.Cost = bestPoint.Cost + 1;
                w.estimatedCost = w.Cost + EstimateDistance(tile, destination);
                w.origin = bestPoint;
                wasSeen[tile.position.y * width + tile.position.x] = true;
                open.Add(w);
            }
            count = open.Count;
        }
        return null;
    }

    public float EstimateDistance(Tile origin, Tile destination)
    {
        return Math.Abs(origin.position.x - destination.position.x) + Math.Abs(origin.position.y - destination.position.y);
    }
}

public class Tile
{
    public Vector2Int position;
    public TileBase texture;
    public bool isWater;
    public WorldStaticObject relatedObject;
    public bool isBlocking;

    public float humidity;
    public float density;

    public List<Tile> neighbours;
}
[System.Serializable]
public class Waypoint
{
    public Tile relatedTile;
    public Waypoint origin;
    public float Cost;
    public float estimatedCost;
}
[System.Serializable]
public class Path
{
    public List<Waypoint> waypoints;

    public Waypoint GetNextPoint()
    {
        return waypoints[waypoints.Count - 1];
    }

    public bool RemoveLast()
    {
        waypoints.RemoveAt(waypoints.Count - 1);
        if(waypoints.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
