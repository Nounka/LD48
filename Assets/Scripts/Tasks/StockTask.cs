using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockTask : GoToTask
{
    public override Vector2Int ChooseDestination(List<Vector2Int> _possibility)
    {
        return base.ChooseDestination(_possibility);
    }

    public override List<Vector2Int> ClosePosition()
    {
        return base.ClosePosition();
    }

    public override void DoMainTask()
    {
        base.DoMainTask();
    }

    public override void DoTask()
    {
        base.DoTask();
    }

    public override TaskBlockage TaskDoable()
    {
        return base.TaskDoable();
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
