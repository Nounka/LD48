using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SondeMap : MonoBehaviour
{
    public Tile tileTested;

    public void Test(Vector2Int _pos)
    {
       tileTested = GameState.instance.map.GetTile(_pos.x, _pos.y);
        Debug.Log(_pos);
        if (tileTested != null)
        {
            Debug.Log(tileTested.isBlocking);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            Test(GameState.instance.controller.CaseFromMouse());
        }
    }
}
