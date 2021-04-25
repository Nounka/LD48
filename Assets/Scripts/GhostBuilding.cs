using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    public Vector2Int Size;
    public SpriteRenderer spriteRenderer;
    public bool canBuild;
    public Vector2Int position;
    public BuildingStats currentStats;


    public void Place(Vector3 _desiredPosition)
    {
        int x = Mathf.FloorToInt(_desiredPosition.x);
        int y = Mathf.FloorToInt(_desiredPosition.y);
        transform.position = new Vector3(x , y , 0);
    }

    public void Place(Vector2Int _positionCase)
    {
        if (currentStats.big)
        {
            transform.position = new Vector3(_positionCase.x + 0.5f, _positionCase.y - 1, 0);
            position.x = _positionCase.x;
            position.y = _positionCase.y;
        }
        else
        {
            transform.position = new Vector3(_positionCase.x+0.5f, _positionCase.y, 0);
            position = _positionCase;
        }
        
    }

    public void DisableGhost()
    {
        spriteRenderer.enabled = false;
    }
    public void CanBuild(bool _state)
    {
        canBuild = _state;
        if (_state)
        {
            spriteRenderer.color = new Color(0, 240, 0, 120);
            transform.position = new Vector3(transform.position.x, transform.position.y, -1);
        }
        else
        {
            spriteRenderer.color = new Color(240, 0, 0, 120);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }
    }

    public void changeBuilding (BuildingStats building)
    {
        // alllow setting "no building"
        if (!building) {
            spriteRenderer.enabled = false;
            return;
        }
        currentStats = building;
        spriteRenderer.sprite = building.sprite;
        spriteRenderer.enabled = true;
        if (building.big)
        {
            transform.localScale = new Vector3(3, 3, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
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
