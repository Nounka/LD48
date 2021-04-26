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

    private Map map;
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
        GameObject building = Instantiate(buildingPrefab, new Vector3(ghostBuilding.position.x + 0.5f, ghostBuilding.position.y , 0), Quaternion.identity, buildingRoot);
        Building script = building.GetComponent<Building>();
        Tile midle = GameState.instance.map.GetTile(ghostBuilding.position.x, ghostBuilding.position.y);
        if (midle.relatedObject != null)
        {
               midle.relatedObject.Destroy();
        }
        midle.relatedObject = script;
        midle.isBlocking = true;
        if (ghostBuilding.currentStats.isBlocking)
        {
            script.isBlocking = true;
        }
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
            building.transform.position = new Vector3(building.transform.position.x, building.transform.position.y - 1, 0);
        }
        else
        {
            building.transform.localScale = new Vector3(1, 1, 1);
        }
        script.position = ghostBuilding.position;
        script.SetUp(ghostBuilding.currentStats);
        script.SetProduction();
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

    private void setState (ControlerMode state) {
        mode = state;
        GameUI.instance.ControllerUpdateState(mode);
    }
    public void EnterBuildingMode(BuildingStats _buildStats)
    {
        ghostBuilding.changeBuilding(_buildStats);
        setState(ControlerMode.placeBuilding);
    }

    public void StopBuildingMode()
    {
        ghostBuilding.changeBuilding(null);
        UnSelect();
    }

    private bool isInMap (int x, int y) {
        return (x >= 0) && (x < map.width)
            && (y >= 0) && (y < map.length);
    }

    public void PlaceGhost()
    {
        Vector2Int desiredCase = CaseFromMouse();
        ghostBuilding.Place(desiredCase);
        bool blocked = false;
        // Store map
        map = GameState.instance.map;
        if(!isInMap(desiredCase.x, desiredCase.y) || map.GetTile(desiredCase.x, desiredCase.y).isBlocking) {
            blocked = true;
        } else if (ghostBuilding.currentStats.big) {
            foreach (Vector2Int vect in GameState.neighboursVectorD)
            {
                int x = desiredCase.x + vect.x;
                int y = desiredCase.y + vect.y;
                if (!isInMap(x, y))
                {
                    blocked = true;
                    break;
                }
                if (GameState.instance.map.GetTile(x, y).isBlocking)
                {
                    blocked = true;
                    break;
                }
                if (GameState.instance.map.GetTile(x, y).relatedObject != null)
                {
                    if (GameState.instance.map.GetTile(x, y).relatedObject.isBlocking)
                    {
                        blocked = true;
                    }
                }

            }
        }
        else
        {
            WorldStaticObject relat = GameState.instance.map.GetTile(desiredCase.x, desiredCase.y).relatedObject;
            if (relat != null)
            {
                if (relat.isBlocking)
                {
                    blocked = true;
                }
            }
        }


        ghostBuilding.CanBuild(!blocked);
    }

    public void UnSelect()
    {
        setState(ControlerMode.idle);
    }

    public void SelectCitizen(Citizen citi)
    {
        selected = citi;
        setState(ControlerMode.selectUnit);
    }

    public void SelectBuilding(Building building)
    {
        selected = building;
        setState(ControlerMode.selectBuilding);
    }
    public void SelectEnnemy(Ennemy enemy)
    {
        selected = enemy;
        setState(ControlerMode.selectEnnemy);
    }
    public void RightClickUnit()
    {
        Vector2Int direction = CaseFromMouse();
        GameObject target = Raycast();
        Citizen select = ((Citizen)selected);
        if (target == null)
        {

            select.TaskMoveTo(direction);
            select.state.type = WorldEntities.State.StateType.moving;
        }
        else
        {
            Ennemy ennemy = target.GetComponent<Ennemy>();
            Building build = target.GetComponent<Building>();
            ResourceNodes nodes = target.GetComponent<ResourceNodes>();

            if (ennemy != null)
            {
                select.state.orderedTask = new FightMTask(select, ennemy);
            }
            else if (build != null)
            {
                if (build.isConstructing)
                {
                    select.state.orderedTask = new BuildTask(build, select);
                }
                else
                {
                    if (build.patron.type == Building.BuildingType.entrepot)
                    {
                        if (!build.isConstructing)
                        {
                            select.state.orderedTask = new StockTask(build, select);
                        }
                        
                    }
                    else
                    {
                        select.state.orderedTask = new GoInsideBuilding(build, select);
                    }
                }
            }
            else
            {
                if(nodes != null)
                {
                    select.state.orderedTask = new GatherTask(nodes,select);
                }
            }

        }

    }
    public void EnroleCitizen()
    {
        Citizen select = ((Citizen)selected);
        select.Engage();

    }
    public void DisbandCitizen()
    {
        Citizen select = ((Citizen)selected);
        select.DisEngage();

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( mode == ControlerMode.placeBuilding )
        {
            PlaceGhost();
        }

        // handle Left click
        if (Input.GetMouseButtonDown(0))
        {
            switch (mode)
            {
                case ControlerMode.placeBuilding:
                    if (ghostBuilding.canBuild)
                    {
                        PlaceBuilding();
                        if (!Input.GetKey(KeyCode.LeftShift))
                        {
                            StopBuildingMode();
                        }
                    }
                    break;
                default:
                    GameObject target = Raycast();
                    if (target != null)
                    {
                        Citizen citi = target.GetComponent<Citizen>();
                        Building build = target.GetComponent<Building>();
                        Ennemy enemy = target.GetComponent<Ennemy>();

                        if (citi != null)
                        {
                            SelectCitizen(citi);
                        }
                        else if (build != null)
                        {
                            SelectBuilding(build);
                        }
                        else if (enemy != null)
                        {
                            SelectEnnemy(enemy);
                        }
                        else
                        {
                            UnSelect();
                        }
                    }
                    else
                    {
                        UnSelect();
                    }
                    break;
            }
        }
        // handle Right click
        if (Input.GetMouseButtonDown(1))
        {
            switch (mode) {
                case ControlerMode.placeBuilding:
                    StopBuildingMode();
                    break;
                case ControlerMode.selectUnit:
                    RightClickUnit();
                    break;
                default:
                    UnSelect();
                    break;
            }
        }
    }

    public void setStateToBuilding(BuildingStats buildingData)
    {
        EnterBuildingMode(buildingData);
    }
}
