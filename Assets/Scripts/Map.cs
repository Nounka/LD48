using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    
    public int Width;
    public int length;
    public Tilemap tilemap;

    public TileBase grassTile;
    public TileBase dirtTile;

    void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
        for (int x = 0; x < Width; x++)
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
}
