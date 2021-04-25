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


}
