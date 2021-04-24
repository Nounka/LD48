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

    public Tile[] tiles;

    void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < length; y++)
            {
                if (Random.value > 0.8)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), dirtTile);
                }
                else
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), grassTile);
                }
            }
        }
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
                Tile tile = GetTile(x, y);
                tile.isWater = false;
                if (Random.value > 0.8)
                {
                    tile.texture = grassTile;
                }
                else
                {
                    tile.texture = dirtTile;
                }
            }
        }
    }

    void GenerateElements()
    {
        // TODO add trees
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
    public WorldObject relatedObject;
}
