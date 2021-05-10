using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    public GameObject buildingPrefab;
    public Transform buildingRoot;

    public ControlerMode mode;

    public GhostBuilding ghostBuilding;

    public WorldObject selected;

    private Map map;

    public EventSystem eventSystem;

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
        GameObject building = Instantiate(buildingPrefab, ghostBuilding.transform.position, Quaternion.identity, buildingRoot);
        Building script = building.GetComponent<Building>();
        Tile midle = map.GetTile(ghostBuilding.position.x, ghostBuilding.position.y);

        if (ghostBuilding.currentStats.bridge)
        {
            SpriteRenderer renderer = building.GetComponent<SpriteRenderer>();
            renderer.drawMode = ghostBuilding.spriteRenderer.drawMode;
            renderer.size = ghostBuilding.spriteRenderer.size;
            renderer.sortingOrder = 1;
            int countUp = 0;
            int countDown = 0;
            bool TopBorderReached = false;
            bool BottomBorderReached = false;

            while (!TopBorderReached)
            {
                Tile tile = map.GetTile(ghostBuilding.position.x, ghostBuilding.position.y + countUp + 1);
                if (tile.isWater)
                {
                    tile.relatedObject = script;
                    countUp++;
                }
                else
                {
                    TopBorderReached = true;
                }
            }

            while (!BottomBorderReached)
            {
                Tile tile = map.GetTile(ghostBuilding.position.x, ghostBuilding.position.y - countDown - 1);
                if (tile.isWater)
                {
                    tile.relatedObject = script;
                    countDown++;
                }
                else
                {
                    BottomBorderReached = true;
                }
            }
        }

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
                Tile voisine = map.GetTile(ghostBuilding.position.x + voisin.x, ghostBuilding.position.y + voisin.y);
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

    public void PlaceGhost()
    {
        Vector2Int desiredCase = CaseFromMouse();
        ghostBuilding.Place(desiredCase);
        bool blocked = false;
        // Store map

        if (ghostBuilding.currentStats.bridge)
        {
            if (!map.isInMap(desiredCase) || !map.GetTile(desiredCase).isWater || map.GetTile(desiredCase).relatedObject != null)
            {
                blocked = true;
            }
        }

        else
        {
            if (!map.isInMap(desiredCase) || map.GetTile(desiredCase).isBlocking)
            {
                blocked = true;
            }
            else if (ghostBuilding.currentStats.big)
            {
                foreach (Vector2Int vect in GameState.neighboursVectorD)
                {
                    int x = desiredCase.x + vect.x;
                    int y = desiredCase.y + vect.y;
                    if (!map.isInMap(x, y))
                    {
                        blocked = true;
                        break;
                    }
                    if (map.GetTile(x, y).isBlocking)
                    {
                        blocked = true;
                        break;
                    }
                    if (map.GetTile(x, y).relatedObject != null)
                    {
                        if (map.GetTile(x, y).relatedObject.isBlocking)
                        {
                            blocked = true;
                        }
                    }

                }
            }
            else
            {
                WorldStaticObject relat = map.GetTile(desiredCase).relatedObject;
                if (relat != null)
                {
                    if (relat.isBlocking)
                    {
                        blocked = true;
                    }
                }
            }
        }


        ghostBuilding.CanBuild(!blocked);
    }

    public void UnSelect()
    {
        setState(ControlerMode.idle);
        GameState.instance.selectIndicator.UnSelect();
    }

    public void SelectCitizen(Citizen citi)
    {
        selected = citi;
        setState(ControlerMode.selectUnit);
        GameState.instance.selectIndicator.Select(citi);
    }

    public void SelectBuilding(Building building)
    {
        selected = building;
        setState(ControlerMode.selectBuilding);
        GameState.instance.selectIndicator.Select(building);
    }
    public void SelectEnnemy(Ennemy enemy)
    {
        selected = enemy;
        setState(ControlerMode.selectEnnemy);
        GameState.instance.selectIndicator.Select(enemy);
    }
    public void RightClickUnit()
    {
        Vector2Int mousePos = CaseFromMouse();
        GameObject target = Raycast();
        Citizen select = ((Citizen)selected);

        if (target == null)
        {
            Tile tile = map.GetTile(mousePos.x, mousePos.y);
            if (tile.relatedObject)
            {
                target = tile.relatedObject.gameObject;
            }
            else
            {
                select.TaskMoveTo(mousePos);
            }
        }
        if (target != null)
        {
            Ennemy ennemy = target.GetComponent<Ennemy>();
            Building build = target.GetComponent<Building>();
            ResourceNodes ressource = target.GetComponent<ResourceNodes>();

            if (ennemy != null)
            {
                select.Order(new FightMTask(select, ennemy));
            }
            else if (build != null)
            {
                if (build.isConstructing)
                {
                    select.Order(new BuildTask(build, select));
                }
                else
                {
                    if (build.patron.type == Building.BuildingType.entrepot)
                    {
                        select.Order(new StockTask(build, select));
                    }
                    else
                    {
                        select.Order(new GoInsideBuilding(build, select));
                    }
                }
            }
            else if (ressource != null && ressource.quantityLeft > 0)
            {
                select.Order(new GatherTask(ressource, select));
            }
            else
            {
                target = null;
            }
        }
        if (target == null)
        {
            select.TaskMoveTo(mousePos);
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
        map = GameState.instance.map;
        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    void Update()
    {
        // If it is over the UI
        if (eventSystem.IsPointerOverGameObject())
        {
            return;
        }
        // Make the ghost follow the mouse when building
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
            switch (mode)
            {
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
