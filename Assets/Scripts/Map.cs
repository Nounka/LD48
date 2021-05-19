using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

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

        new Pathfinder().Setup(width, length, false);
    }

    void Update()
    {

    }

    void GenerateTiles()
    {
        wasSeen = new BitArray(width * length);
        tiles = new Tile[width * length];
        blockingMap = new NativeArray<bool>(width*length, Allocator.Persistent);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {

                float humidity = (humidityPerlin.Sample(x * 1.0f / 400, y * 1.0f / 400) + 1) / 4 + (humidityMacroPerlin.Sample(x * 1.0f / 1000, y * 1.0f / 1000) + 1) / 4;
                foreach (RiverDefinition river in rivers)
                {
                    int centerYAtXPos = ((width - x) * river.posYLeft + x * river.posYRight) / width + Mathf.RoundToInt(3 * Mathf.Sin(x / 3 * width));
                    int distanceFromRiverCenter = Mathf.Abs(centerYAtXPos - y);
                    if (distanceFromRiverCenter < river.width)
                    {
                        humidity = 1.0f;
                    }
                    else if (distanceFromRiverCenter < river.width + river.widthBorder)
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
                else if (humidity > 0.4)
                {
                    tile.texture = grassTile;
                }
                else
                {
                    tile.texture = dirtTile;
                }
                tiles[y * width + x] = tile;
                blockingMap[y * width + x] = tile.isBlocking;
            }
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                Tile tile = tiles[y * width + x];
                tile.neighbours = new List<Tile>();
                if (x > 0)
                {
                    tile.neighbours.Add(tiles[y * width + x - 1]);
                }
                if (x < width - 1)
                {
                    tile.neighbours.Add(tiles[y * width + x + 1]);
                }
                if (y > 0)
                {
                    tile.neighbours.Add(tiles[(y - 1) * width + x]);
                }
                if (y < length - 1)
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
                                        if (tile.relatedObject != null || tile.isWater)
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

    public bool isInMap(int x, int y)
    {
        return (x >= 0 && x < width) && (y >= 0 && y < length);
    }
    public bool isInMap(Vector2Int vec)
    {
        return (vec.x >= 0 && vec.x < width) && (vec.y >= 0 && vec.y < length);
    }

    public Tile GetTile(int x, int y)
    {
        return tiles[y * width + x];
    }

    public Tile GetTile(Vector2Int p)
    {
        return tiles[p.y * width + p.x];
    }

    public Tile GetTile(Vector3 pos)
    {
        return GetTile(Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y));
    }

    public Path GetPath(Tile origin, Tile destination, float distance = 0)
    {
        Waypoint endpoint = SolvePathTo(origin, destination, distance);
        if (endpoint != null)
        {
            return new Path(endpoint);
        }
        return null;
    }
    NativeArray<bool> blockingMap;
    NativeList<int2> list = new NativeList<int2>(1024, Allocator.Persistent);
    public Path Pathfinder(Tile origin, Tile destination, float distance = 0)
    {
        var job = new Pathfinder
        {
            isBlocking = blockingMap,
            origin = new int2(origin.position.x,origin.position.y),
            destination = new int2(destination.position.x, destination.position.y),
            distance = distance
        };
        Waypoint endpoint = SolvePathTo(origin, destination, distance);
        if (endpoint != null)
        {
            return new Path(endpoint);
        }
        return null;
    }

    private BitArray wasSeen;
    List<Waypoint> open = new List<Waypoint>(16 * 1024);
    public Waypoint SolvePathTo(Tile origin, Tile destination, float distance = 0)
    {
        // Can't compute path to a non reachable object
        if (destination.isBlocking || origin == destination)
        {
            return null;
        }

        wasSeen.SetAll(false);
        Waypoint.reset();
        Waypoint current = Waypoint.getWaypoint();
        current.relatedTile = origin;
        current.Cost = 0;
        current.origin = null;
        current.estimatedCost = EstimateDistance(origin, destination);

        open.Clear();
        open.Add(current);

        bool found = false;
        int count = open.Count;
        while (!found && count > 0)
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
            open.RemoveAt(bestIndex);

            List<Tile> neighboours = bestPoint.relatedTile.neighbours;
            int neighboursCount = neighboours.Count;
            for (int index = 0; index < neighboursCount; index++)
            {
                Tile tile = neighboours[index];
                // We don't handle already visited spaces, nor blocking tiles
                if (tile.isBlocking || wasSeen[tile.position.y * width + tile.position.x])
                {
                    continue;
                }
                float estimatedDist = EstimateDistance(tile, destination);
                Waypoint w = Waypoint.getWaypoint();
                w.relatedTile = tile;
                w.Cost = bestPoint.Cost + 1;
                w.origin = bestPoint;
                w.estimatedCost = w.Cost + estimatedDist;
                if (tile == destination || estimatedDist < distance)
                {
                    w.estimatedCost = w.Cost;
                    return w;
                }
                wasSeen[tile.position.y * width + tile.position.x] = true;
                open.Add(w);
            }
            count = open.Count;
        }
        return null;
    }

    public float EstimateDistance(Tile origin, Tile destination)
    {
        return Mathf.Sqrt((origin.position.x - destination.position.x) * (origin.position.x - destination.position.x) + (origin.position.y - destination.position.y) * (origin.position.y - destination.position.y));
        // return Math.Abs(origin.position.x - destination.position.x) + Math.Abs(origin.position.y - destination.position.y);
    }
}

public class Tile
{
    public Vector2Int position;
    public TileBase texture;
    public bool isWater;
    public WorldStaticObject relatedObject;
    public ItemDrop items;
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

    private Waypoint()
    {
        // do nothing
    }
    // Copy constructor
    public Waypoint(Waypoint from)
    {
        relatedTile = from.relatedTile;
        if (from.origin == null)
        {
            origin = null;
        }
        else
        {
            origin = new Waypoint(from.origin);
        }
        Cost = from.Cost;
        estimatedCost = from.estimatedCost;
    }

    private static int current = 0;
    private static int length = 0;
    private static int allocSize = 1000;
    private static List<Waypoint> list = new List<Waypoint>();

    public static Waypoint getWaypoint()
    {
        if (current >= length)
        {
            length += allocSize;
            for (int i = 0; i < allocSize; i++)
            {
                list.Add(new Waypoint());
            }
        }
        return list[current++];
    }
    public static void reset()
    {
        current = 0;
    }
}
[System.Serializable]
public class Path
{
    private Stack<Waypoint> waypoints;
    private Waypoint target;

    public Path(Waypoint pathEnd)
    {
        Waypoint wp = new Waypoint(pathEnd);
        target = wp;
        waypoints = new Stack<Waypoint>();
        while (wp != null)
        {
            waypoints.Push(wp);
            wp = wp.origin;
        }
    }

    public Waypoint GetTarget()
    {
        return target;
    }
    public Waypoint GetNextPoint()
    {
        if (waypoints.Count == 0)
        {
            return null;
        }
        return waypoints.Peek();
    }
    public bool isEmpty()
    {
        if (waypoints.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool RemoveLast()
    {
        waypoints.Pop();
        if (waypoints.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}


public struct PathFinderData {
    public int width, height;
    public NativeArray<int2> neighbours;
    public NativeArray<bool> wasSeen;
    public NativeArray<float> cost;
    public NativeArray<float> estimatedCost;
    public NativeArray<int> pathOrigin;
    public NativeList<int> open;


    public PathFinderData Create(int w, int h, bool canMoveDiag)
    {
        width = w;
        height = h;
        wasSeen = new NativeArray<bool>(width * height, Allocator.Persistent);
        cost = new NativeArray<float>(width * height, Allocator.Persistent);
        estimatedCost = new NativeArray<float>(width * height, Allocator.Persistent);
        pathOrigin = new NativeArray<int>(width * height, Allocator.Persistent);
        open = new NativeList<int>(16 * 1024, Allocator.Persistent);

        if (canMoveDiag)
        {
            neighbours = new NativeArray<int2>(8, Allocator.Persistent)
            {
                [0] = new int2(-1, -1), // Bottom L
                [1] = new int2(0, -1), // Bottom
                [2] = new int2(1, -1), // Bottom R
                [3] = new int2(-1, 0), // Left
                [4] = new int2(1, 0), // Right
                [5] = new int2(-1, 1), // Top L
                [6] = new int2(0, 1), // Top
                [7] = new int2(1, 1), // Top R
            };
        }
        else
        {
            neighbours = new NativeArray<int2>(4, Allocator.Persistent)
            {
                [0] = new int2(0, -1), // Bottom
                [1] = new int2(-1, 0), // Left
                [2] = new int2(1, 0), // Right
                [3] = new int2(0, 1), // Top
            };
        }
    }
}

[BurstCompile(CompileSynchronously = true)]
public struct Pathfinder: IJob
{
    private static int width, height;
    private static NativeArray<int2> neighbours;
    private static NativeArray<bool> wasSeen;
    private static NativeArray<float> cost;
    private static NativeArray<float> estimatedCost;
    private static NativeArray<int> pathOrigin;
    private static NativeList<int> open;

    // store map data
    [ReadOnly]
    public NativeArray<bool> isBlocking;
    // store solver origin
    [ReadOnly]
    public int2 origin;
    // store solver target
    [ReadOnly]
    public int2 destination;
    // store solver config
    [ReadOnly]
    public float distance;
    // store solver output
    [WriteOnly]
    public NativeList<int2> list;

    public void Setup(int w, int h, bool canMoveDiag)
    public int GetIndex(int2 p)
    {
        return p.y * width + p.x;
    }
    public void GetPos(int index, ref int2 p)
    {
        p.x = index % width;
        p.y = index / width;
    }
    public bool isInMap(int2 p)
    {
        return (p.x >= 0 && p.x < width) && (p.y >= 0 && p.y < height);
    }
    public float EstimateDistance(int2 a, int2 b)
    {
        return Mathf.Sqrt((a.x - b.x) * (a.x - b.x) + (a.y - b.y) * (a.y - b.y));
    }
    public float EstimateDistance(int2 d)
    {
        return Mathf.Sqrt(d.x * d.x + d.y * d.y);
    }
    public int SolvePathTo(int2 origin, int2 destination, float distance = 0)
    {
        // Edge-case : we don't want to handle this
        if (origin.x == destination.x && origin.y == destination.y)
        {
            return -1;
        }
        // Can't compute path to a non reachable object
        if (isBlocking[GetIndex(destination)] && distance < 1)
        {
            return -1;
        }

        int mapSize = width * height;
        for (int i = 0; i < mapSize; i++)
        {
            wasSeen[i] = false;
        }

        open.Clear();
        int startFrom = GetIndex(origin);
        open.Add(startFrom);
        wasSeen[startFrom] = true;
        origin[startFrom] = -1;
        cost[startFrom] = 0;
        estimatedCost[startFrom] = EstimateDistance(origin, destination);
        int count = open.Length;
        int2 pos = new int2(0, 0);
        while (count > 0)
        {
            int bestPoint = open[0];
            int bestIndex = 0;
            float bestCost = estimatedCost[bestPoint];
            for (int index = 1; index < count; index++)
            {
                int wp = open[index];
                if (estimatedCost[wp] < bestCost)
                {
                    bestPoint = wp;
                    bestIndex = index;
                    bestCost = estimatedCost[wp];
                }
            }
            // TODO test or use the safer 'RemoveAt'
            open.RemoveAtSwapBack(bestIndex);
            GetPos(bestPoint, ref pos);
            for (int index = 0; index < neighbours.Length; index++)
            {
                int2 next = neighbours[index] + pos;
                if (!isInMap(next))
                {
                    continue;
                }
                int newer = GetIndex(next);
                if (wasSeen[newer] || isBlocking[newer])
                {
                    continue;
                }
                wasSeen[newer] = true;
                pathOrigin[newer] = bestPoint;
                cost[newer] = cost[bestPoint] + EstimateDistance(neighbours[index]);
                float estimatedDist = EstimateDistance(next, destination);
                estimatedCost[newer] = cost[newer] + estimatedDist;
                if (estimatedDist <= distance)
                {
                    return newer;
                }
                open.Add(newer);
            }
            count = open.Length;
        }
        return -1;
    }


    public void Execute() {
        NativeList<int> internalList = new NativeList<int>(512, Allocator.Temp);
        int solvedPath = SolvePathTo(origin, destination, distance);
        while (solvedPath != -1)
        {
            internalList.Add(solvedPath);
            solvedPath = origin[solvedPath];
        }
        int size = internalList.Length;
        int2 pos = new int2(0, 0);

        list.Clear();
        for (int i = size - 1; i >= 0; i--)
        {
            GetPos(internalList[i], ref pos);
            list.Add(pos);
        }
    }

}

