using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTask : GoToTask
{
    public Building construction;



    public override TaskBlockage TaskDoable() {
        if (construction!=null)
        {
            if(construction.isConstructing)
            {
                return TaskBlockage.doable;
            }
            else
            {
                return TaskBlockage.notAvailable;
            }         
        }
        else
        {
            return TaskBlockage.notAvailable;
        }
            
    }

    public override void WorkTask()
    {
        base.WorkTask();
    }
    public override void DoTask()
    {
        construction.WorkOnBuilding(GetWorkValue());
        if (construction.isConstructing)
        {
            taskTimer = 0;
        }
        else
        {
            CancelTask(TaskBlockage.done);
        }
    }

    public float GetWorkValue()
    {
        return 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        taskDistance = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override List<Vector2Int> ClosePosition()
    {
        List<Vector2Int> retour = new List<Vector2Int>();
        //retour.Add(construction.position);////Ajout la case en elle meme

        if (construction.patron.type == Building.BuildingType.bridge)
        {
            int countDown = 0;
            bool BottomBorderReached = false;
            while (!BottomBorderReached)
            {
                Map map = GameState.instance.map;
                Tile tile = map.GetTile(construction.position.x, construction.position.y - countDown - 1);
                if (tile.isWater)
                {
                    countDown++;
                }
                else
                {
                    BottomBorderReached = true;
                    retour.Add(tile.position);
                }
            }
        }
        else
        {
            foreach (Vector2Int voisine in GameState.neighboursVectorD)
            {
                retour.Add(new Vector2Int(construction.position.x + voisine.x, construction.position.y + voisine.y));
            }
        }
        return retour;
    }

    public override void DoMainTask()
    {
        actor.PlaySound(AudioBank.AudioName.construction);
    }
    public override float TaskRatio()
    {
        return 1f;
    }

    public BuildTask(Building _target,WorldEntities _actor)
    {
        construction = _target;
        actor = _actor;
        taskSpeed = GameState.instance.buildSpeed;
        type = TaskType.build;
    }
}
