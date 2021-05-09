using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuinPlacer : MonoBehaviour
{
    [System.Serializable]
    public class FromTo
    {
        public int from, to;
    }

    public GameObject ruinPrefab;
    public Transform ruinRoot;

    public int ruinsNumbers;

    public List<Tool> toolAvailable;
    public ResourceStack ressourcesAvailable;
    public List<Schematic> schematicsAvailable;

    public FromTo xRange;
    public FromTo yRange;

    public void PlaceRuins()
    {
        Map map = GameState.instance.map;
        int internalcount = 0;
        for(int x = 0; x < ruinsNumbers;)
        {
            int xPlace = Random.Range(xRange.from, xRange.to);
            int yPlace = Random.Range(yRange.from, yRange.to);
            Vector2Int pos = new Vector2Int(xPlace, yPlace);

            if(CanBePlacedHere(pos))
            {
                ClearPlace(pos);
                PlaceRuin(pos);
                internalcount = 0;
                x++;
            }
            else
            {
                internalcount++;
            }
            
            if (internalcount > 50)
            {
                internalcount = 0;
                x=ruinsNumbers;
            }
            
        }
    }

    public void PlaceRuin(Vector2Int _position)
    {
        GameObject building = Instantiate(ruinPrefab, new Vector3(_position.x+0.5f, _position.y-1,0), Quaternion.identity, ruinRoot);
        Ruin script = building.GetComponent<Ruin>();
        Tile midle = GameState.instance.map.GetTile(_position.x, _position.y);

        Map map = GameState.instance.map;

        if (midle.relatedObject != null)
        {
            midle.relatedObject.Destroy();
        }
        midle.isBlocking = true;
        midle.relatedObject = script;
        foreach (Vector2Int add in GameState.neighboursVectorD)
        {
            int posx = _position.x + add.x;
            int posy = _position.y + add.y;
            Tile tile = map.GetTile(_position.x + add.x, _position.y + add.y);

            if (tile.relatedObject != null)
            {
                tile.relatedObject.Destroy();
            }
            tile.relatedObject = script;
            if (add.y < 0)
            {
                if (add.x != 0)
                {
                    tile.isBlocking = false;
                }
                else
                {
                    tile.isBlocking = true;
                }
            }
            else
            {
                tile.isBlocking = true;
            }
        }

        script.position = new Vector2Int(_position.x, _position.y);
        script.dropPosition = new Vector2Int(_position.x - 1, _position.y - 1);

    }

    public bool CanBePlacedHere(Vector2Int _position)
    {
        Map map = GameState.instance.map;

        Tile test = map.GetTile(_position.x, _position.y);
        Building build = test.relatedObject as Building;
        Ruin ruin = test.relatedObject as Ruin;
        if (!test.isWater&& build == null&& ruin==null)
        {
            foreach(Vector2Int add in GameState.neighboursVectorD)
            {
                test = map.GetTile(_position.x + add.x, _position.y + add.y);
                build = test.relatedObject as Building;
                if(test.isWater&& build != null&& ruin == null)
                {
                    return false;
                }             
            }
            return true;
        }
        return false;
    }
    public void ClearPlace(Vector2Int _position)
    {

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
