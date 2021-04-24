using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldStaticObject : WorldObject
{
    public Vector2Int size;

    public Vector2Int position;//La position du coin en bas a gauche

    public bool isBlocking;

    public void Destroy()
    {
        for(int x = 0; x < size.x; x++)
        {
            for(int y = 0; y < size.y; y++)
            {
                GameState.instance.map.ClearPlace(new Vector2Int(position.x+x,position.y+y));
            }
        }
        Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
