using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementInitial : MonoBehaviour
{
    public Vector2Int initialBande;//La position des batiments et citoyen du début min 1 pour pouvoir placer les gros batiments
    public Vector2Int finalBande;
    public int startingCitizen;

    public GameObject buildingPrefab;

    public BuildingStats warehouse;
    public BuildingStats ruineFin;

    public Transform buildingRoot;

    public int RandomX()
    {
        Map map = GameState.instance.map;

        int retour = Random.Range(3, map.width-5);
        return retour;
    }

    public void PlaceInitial()
    {
        int posx = RandomX();
        int posy = Random.Range(initialBande.x, initialBande.y);

        int ennemyposx = RandomX();
        int ennemyposy = 2;

        Map map = GameState.instance.map;
        Tile tile = map.GetTile(ennemyposx, ennemyposy);

        if (tile.relatedObject != null)
        {
            tile.relatedObject.Destroy();
        }
            foreach (Vector2Int vect in GameState.neighbours2VectorD)
            {
                Tile tileSec = map.GetTile(ennemyposx + vect.x, ennemyposy + vect.y);

                if (tileSec.relatedObject != null)
                {
                    tileSec.relatedObject.Destroy();
                }
            }
        GameState.instance.overMind.BasePlace = new Vector2Int(ennemyposx, ennemyposy);
        

        PlaceBuilding(new Vector2Int(posx, posy), warehouse,true);

        GameState.instance.cam.transform.position = new Vector3(posx, posy, -10);
        List<Vector2Int> spawnLocation = new List<Vector2Int>();
        foreach(Vector2Int vect in GameState.neighbours2VectorD)
        {
            if (!GameState.neighboursVectorD.Contains(vect))
            {
                spawnLocation.Add(vect);
            }
            
        }

        for(int x = 0; x < startingCitizen; x++)
        {
            int rand = Random.Range(0, spawnLocation.Count-1);
            Vector2Int positionSpawn = spawnLocation[rand];
            positionSpawn.x += posx;
            positionSpawn.y += posy;
            GameState.instance.citizenGenerator.CreateCitizen(positionSpawn);
        }
        posx = RandomX();
        posy = Random.Range(finalBande.x, finalBande.y);
        PlaceBuilding(new Vector2Int(posx, posy), ruineFin,false);



    }

    public void PlaceBuilding(Vector2Int _pos,BuildingStats _stats,bool _initial)
    {
        
        Debug.Log("PLaceBuilding:"+_pos);
        //Clear la zone
        Map map = GameState.instance.map;
        Tile tile = map.GetTile(_pos.x,_pos.y);

        if (tile.relatedObject != null)
        {
            tile.relatedObject.Destroy();
        }
        if (_initial)
        {
            foreach (Vector2Int vect in GameState.neighbours2VectorD)
            {
                Tile tileSec = map.GetTile(_pos.x + vect.x, _pos.y + vect.y);

                if (tileSec.relatedObject != null)
                {
                    tileSec.relatedObject.Destroy();
                }
            }
        }
        else
        {
            foreach (Vector2Int vect in GameState.neighboursVectorD)
            {
                Tile tileSec = map.GetTile(_pos.x + vect.x, _pos.y + vect.y);

                if (tileSec.relatedObject != null)
                {
                    tileSec.relatedObject.Destroy();
                }
            }
        }

        GameObject building = Instantiate(buildingPrefab, new Vector3(_pos.x + 0.5f, _pos.y - 1, 0), Quaternion.identity, buildingRoot);
        Building build = building.GetComponent<Building>();

        if (build != null)
        {
            build.position = new Vector2Int(_pos.x, _pos.y);
            build.SetUp(_stats);
            build.isConstructing = false;
            build.spriteRenderer.sprite = _stats.sprite;
            foreach (Vector2Int voisin in GameState.neighboursVectorD)
            {
                Tile voisine = GameState.instance.map.GetTile(_pos.x + voisin.x, _pos.y + voisin.y);
                if (voisine.relatedObject != null)
                {
                    voisine.relatedObject.Destroy();
                }
                voisine.relatedObject = build;
                if (voisin.y == -1 && voisin.x != 0)
                {
                    voisine.isBlocking = false;
                }
                else
                {
                    voisine.isBlocking = true;
                }
            }
            build.entrance = new Vector2Int(_pos.x + 1, _pos.y - 1);
            build.productionCase = new Vector2Int(_pos.x - 1, _pos.y - 1);
            building.transform.localScale = new Vector3(3, 3, 1);
        }


    }
    // Start is called before the first frame update
    void Start()
    {
        PlaceInitial();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
