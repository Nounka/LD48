using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementInitial : MonoBehaviour
{
    public Vector2Int initialBande;//La position des batiments et citoyen du début min 1 pour pouvoir placer les gros batiments

    public int startingCitizen;

    public GameObject buildingPrefab;

    public BuildingStats warehouse;
    public BuildingStats ruineFin;

    public Transform buildingRoot;

    public int RandomY()
    {
        Map map = GameState.instance.map;

        int retour = Random.Range(3, map.width-5);
        return retour;
    }

    public void PlaceInitial()
    {
        int posy = RandomY();
        int posx = Random.Range(initialBande.x, initialBande.y);

        GameObject building = Instantiate(buildingPrefab, new Vector3(posx + 0.5f, posy, 0), Quaternion.identity, buildingRoot);
    }

    public void PlaceBuilding(Vector2Int _pos,BuildingStats _stats)
    {
        //Clear la zone
        

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
