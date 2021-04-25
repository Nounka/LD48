using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Transform buildingRoot;

    public ControlerMode mode;

    public GhostBuilding ghostBuilding;

    public WorldObject selected;

    public Sprite buildingPlacementSprite;
    public enum ControlerMode
    {
        placeBuilding,
        selectBuilding,
        selectUnit,
        selectEnnemy,
        idle
    }

    public GameObject Raycast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


        if (Physics.Raycast(ray, out hit))
        {
            GameObject objectHit = hit.transform.gameObject;

            return objectHit;

        }
        return null;
    }

    public Vector3 OnPlane(Ray _ray)
    {
        if (_ray.direction.z > 0)
        {
            float distance = _ray.origin.z / _ray.direction.z;
            Vector3 retour = new Vector3(_ray.origin.x - distance * _ray.direction.x, _ray.origin.y - distance * _ray.direction.y, 0);
            return retour;
        }
        return new Vector3(0, 0, -1);
    }

    public void PlaceBuilding()
    {
        GameObject building = Instantiate(buildingPrefab, new Vector3(ghostBuilding.position.x + 0.5f, ghostBuilding.position.y + 0.5f, 0), Quaternion.identity, buildingRoot);
        Building script = building.GetComponent<Building>();
        Tile midle = GameState.instance.map.GetTile(ghostBuilding.position.x, ghostBuilding.position.y);
        if (midle.relatedObject != null)
        {
               midle.relatedObject.Destroy();
        }
        midle.relatedObject = script;
        midle.isBlocking = true;
        if (ghostBuilding.currentStats.big)
        {
            foreach (Vector2Int voisin in GameState.neighboursVectorD)
            {
                Tile voisine = GameState.instance.map.GetTile(ghostBuilding.position.x + voisin.x, ghostBuilding.position.y + voisin.y);
                if (voisine.relatedObject != null)
                {
                    voisine.relatedObject.Destroy();
                }
                voisine.relatedObject = script;
                if (voisin.y == -1&&voisin.x!=0)
                {
                    voisine.isBlocking = false;
                }
                else
                {
                    voisine.isBlocking = true;
                }
            }
            script.entrance = new Vector2Int(ghostBuilding.position.x + 1, ghostBuilding.position.y - 1);
            script.productionCase = new Vector2Int(ghostBuilding.position.x - 1, ghostBuilding.position.y - 1);
            building.transform.localScale = new Vector3(3, 3, 1);
        }
        else
        {
            building.transform.localScale = new Vector3(1, 1, 1);
        }
        script.SetUp(ghostBuilding.currentStats);
        script.SetProduction();
        script.spriteRenderer.sprite = buildingPlacementSprite;

       
    }

    public Vector3 OnPlaneFromMouse()
    {
        return OnPlane(Camera.main.ScreenPointToRay(Input.mousePosition));
    }

    public Vector2Int CaseFromMouse()
    {
        Vector3 mouseProjection = OnPlaneFromMouse();
        Vector2Int desiredCase = new Vector2Int(Mathf.FloorToInt(mouseProjection.x), Mathf.FloorToInt(mouseProjection.y));
        return desiredCase;
    }

    public void GhostBuilding(BuildingStats _buildStats)
    {
        ghostBuilding.Resize(_buildStats.big);
        ghostBuilding.spriteRenderer.enabled = true;
        ghostBuilding.currentStats = _buildStats;
    }

    public void StopGhost()
    {
        ghostBuilding.spriteRenderer.enabled = false;
    }

    public void PlaceGhost()
    {
        Vector2Int desiredCase = CaseFromMouse();
        ghostBuilding.Place(desiredCase);
        bool blocked = false;
        if(GameState.instance.map.GetTile(desiredCase.x, desiredCase.y).isBlocking)
        {
            ghostBuilding.CanBuild(false);
            ghostBuilding.canBuild = false;
            blocked = true;
        }
        foreach(Vector2Int vect in GameState.neighboursVectorD)
        {
            if (GameState.instance.map.GetTile(desiredCase.x + vect.x, desiredCase.y + vect.y).isBlocking)
            {
                ghostBuilding.CanBuild(false);
                ghostBuilding.canBuild = false;
                blocked = true;
            }
        }
        if (!blocked)
        {
            ghostBuilding.canBuild = true;
        }

    }

    public void RightClickUnit()
    {
        Vector2Int direction = CaseFromMouse();
        GameObject target = Raycast();
        if(target == null)
        {
            Citizen select = ((Citizen)selected);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mode == ControlerMode.placeBuilding)
        {
            PlaceGhost();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (mode != ControlerMode.placeBuilding)
            {
                GameObject target = Raycast();
                if (target != null)
                {
                    Citizen citi = target.GetComponent<Citizen>();
                    Building build = target.GetComponent<Building>();
                }


            }
            else
            {
                if (ghostBuilding.canBuild)
                {
                    PlaceBuilding();
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if(mode == ControlerMode.placeBuilding)
            {
                StopGhost();
                mode = ControlerMode.idle;
            }
            if (mode == ControlerMode.selectUnit)
            {

            }
        }

    }
}
