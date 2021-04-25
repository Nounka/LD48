using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBuilding : MonoBehaviour
{
    public Vector2Int Size;
    public SpriteRenderer spriteRenderer;
    public bool canBuild;

    public Vector2Int position;

    public void Place(Vector3 _desiredPosition)
    {
        int x = Mathf.FloorToInt(_desiredPosition.x);
        int y = Mathf.FloorToInt(_desiredPosition.y);
        transform.position = new Vector3(x + 0.5f, y + 0.5f, 0);
    }

    public void Place(Vector2Int _positionCase)
    {
        transform.position = new Vector3(_positionCase.x + 0.5f, _positionCase.y + 0.5f, 0);
    }
    public void CanBuild(bool _state)
    {
        if (_state)
        {
            spriteRenderer.color = new Color(0, 240, 0, 120);
        }
        else
        {
            spriteRenderer.color = new Color(240, 0, 0, 120);
        }
    }

    public void Resize()
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
