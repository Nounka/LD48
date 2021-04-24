using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoise
{
    protected int sizeX;
    protected int sizeY;
    List<Vector2> vectors;

    public void Init(int _sizeX, int _sizeY)
    {
        vectors = new List<Vector2>();
        sizeX = _sizeX;
        sizeY = _sizeY;
        for (int y = 0; y < _sizeY; y++)
        {
            for (int x = 0; x < _sizeX; x++)
            {
                Vector2 vec = Random.insideUnitCircle;
                vec.Normalize();
                vectors.Add(vec);
            }
        }
    }

    public float Sample(float _x, float _y)
    {
        while (_x < 0)
        {
            _x += sizeX;
        }

        while (_y < 0)
        {
            _y += sizeX;
        }

        float x = (_x * sizeX) % sizeX;
        float y = (_y * sizeY) % sizeY;

        float ceilX = Mathf.Ceil(x);
        float ceilY = Mathf.Ceil(y);

        if (ceilX == sizeX)
        {
            ceilX = 0;
        }

        if (ceilY == sizeY)
        {
            ceilY = 0;
        }

        float internalX = (x - Mathf.Floor(x));
        float internalY = (y - Mathf.Floor(y));

        Vector2 d1 = new Vector2(internalX, internalY);
        Vector2 d2 = new Vector2(internalX - 1, internalY);
        Vector2 d3 = new Vector2(internalX, internalY - 1);
        Vector2 d4 = new Vector2(internalX - 1, internalY - 1);

        float result = 0;

        result += Vector2.Dot(vectors[(int)(Mathf.Floor(y) * sizeX + Mathf.Floor(x))], d1) * (1 - internalX) * (1 - internalY);
        result += Vector2.Dot(vectors[(int)(Mathf.Floor(y) * sizeX + ceilX)], d2) * internalX * (1 - internalY);
        result += Vector2.Dot(vectors[(int)(ceilY * sizeX + Mathf.Floor(x))], d3) * (1 - internalX) * internalY;
        result += Vector2.Dot(vectors[(int)(ceilY * sizeX + ceilX)], d4) * internalX * internalY;

        return result;
    }
}
