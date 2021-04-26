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
            spriteRenderer.drawMode = SpriteDrawMode.Simple;
            transform.position = new Vector3(_positionCase.x + 0.5f, _positionCase.y - 1, 0);
            position.x = _positionCase.x;
            position.y = _positionCase.y;
        }
        else if (currentStats.bridge)
        {
            Map map = GameState.instance.map;
            Tile selectedTile = map.GetTile(_positionCase.x, _positionCase.y);
            if(selectedTile.isWater)
            {
                int countUp = 0;
                int countDown = 0;
                bool TopBorderReached = false;
                bool BottomBorderReached = false;

                while (!TopBorderReached)
                {
                    if(map.GetTile(_positionCase.x, _positionCase.y + countUp + 1).isWater)
                    {
                        countUp++;
                    }
                    else
                    {
                        TopBorderReached = true;
                    }
                }

                while (!BottomBorderReached)
                {
                    if(map.GetTile(_positionCase.x, _positionCase.y - countDown - 1).isWater)
                    {
                        countDown++;
                    }
                    else
                    {
                        BottomBorderReached = true;
                    }
                }

                transform.position = new Vector3(_positionCase.x + 0.5f, _positionCase.y + countUp / 2.0f - countDown / 2.0f + 0.5f, 0);
                spriteRenderer.drawMode = SpriteDrawMode.Tiled;
                spriteRenderer.size = new Vector2(1, 1 + countDown + countUp);
                position.x = _positionCase.x;
                position.y = _positionCase.y;
            }
            else
            {
                transform.position = new Vector3(_positionCase.x + 0.5f, _positionCase.y, 0);
                position = _positionCase;
            }
        }
        else
        {
            spriteRenderer.drawMode = SpriteDrawMode.Simple;
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
