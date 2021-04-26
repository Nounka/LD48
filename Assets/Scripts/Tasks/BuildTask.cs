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
            actor.RemoveTask(this, TaskBlockage.done);
        }
    }

    public float GetWorkValue()
    {
        return 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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

    public override Vector2Int ChooseDestination(List<Vector2Int> _possibility)
    {
        if (construction.patron.type == Building.BuildingType.wall)
        {
            Vector2Int retour = new Vector2Int(-1, -1);
            float currentDistance = 0f;
            /*if (_possibility.Contains(destination))
            {
                return destination;
            }*/
            if (_possibility.Count > 0)
            {
                foreach (Vector2Int possi in _possibility)
                {
                    if (possi.x >= 0 && possi.x < GameState.instance.map.width)
                    {
                        if (possi.y >= 0 && possi.y < GameState.instance.map.length)
                        {
                            if (retour.x == -1)
                            {
                                retour = possi;
                                currentDistance = Distance(actor.position, possi);
                            }
                            float test = Distance(actor.position, possi);
                            if (test < currentDistance)
                            {
                                retour = possi;
                                currentDistance = Distance(actor.position, possi);
                            }

                        }
                    }
                }


            }
            return retour;
        }
        else if (construction.patron.type == Building.BuildingType.bridge)
        {
            return _possibility[0];
        }
        else
        {
            return new Vector2Int(construction.position.x + 1, construction.position.y - 1);
        }
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
        unavailablePosition = new List<Vector2Int>();
    }
}
