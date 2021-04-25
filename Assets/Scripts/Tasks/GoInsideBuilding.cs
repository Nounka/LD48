using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoInsideBuilding : GoToTask
{
    public Building building;


    public override Vector2Int ChooseDestination(List<Vector2Int> _possibility)
    {
        return building.entrance;
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
