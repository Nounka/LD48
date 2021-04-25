using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameObject buildingPrefab;
    public ControlerMode mode;

    public GhostBuilding ghostBuilding;

    public enum ControlerMode
    {
        placeBuilding,
        selectBuilding,
        selectUnit,
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
        if (ghostBuilding.canBuild)
        {
            if(GameState.instance.map.GetTile(ghostBuilding.position.x, ghostBuilding.position.y).relatedObject != null)
            {
                GameState.instance.map.GetTile(ghostBuilding.position.x, ghostBuilding.position.y).relatedObject.Destroy();
            }
        }
    }

    public Vector3 OnPlaneFromMouse()
    {
        return OnPlane(Camera.main.ScreenPointToRay(Input.mousePosition));
    }

    public void GhostBuilding(BuildingStats _buildStats)
    {
        ghostBuilding.Resize();
    }

    public void StopGhost()
    {
        ghostBuilding.spriteRenderer.enabled = false;
    }

    public void PlaceGhost()
    {
        Vector3 mouseProjection = OnPlaneFromMouse();
        Vector2Int desiredCase = new Vector2Int(Mathf.FloorToInt(mouseProjection.x),Mathf.FloorToInt(mouseProjection.y));
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlaceGhost();
        if (Input.GetMouseButtonDown(0))
        {
            if (mode != ControlerMode.placeBuilding)
            {
                GameObject target = Raycast();
            }
        }
    }
}
