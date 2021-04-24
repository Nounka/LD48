using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEntities : MonoBehaviour
{
    public string nom;

    public Vector2 position;
    public Vector2Int positionCase;

    public bool isMoving;
    public bool isCitizen;

    public float healthCurrent;
    public float healthMax;
    public float healRegen;
    public float healSpeed;
    public float healTimer;

    public List<Tool> equipements;

    public ResourceStack carrying;
    public int maxCarry;

    public State state;

    public List<Task> actions;

    public void ClearTask()
    {
        state.orderedTask = null;
        state.decidedTask = null;
        actions.Clear();

    }

    public bool CheckTask()
    {
        bool retour = true;
        foreach(Task task in actions)
        {
            if (task.TaskDoable() == Task.TaskBlockage.doable)
            {
               
            }
            else
            {
                retour = false;
            }
        }
        return retour;
    }

    public ResourceStack AddRessources(ResourceStack _ajout)
    {
        ResourceStack surplus = carrying.AddWithinCapacity(_ajout, maxCarry);
        return surplus;
        if (surplus.GetSize() > 0)
        {

            //GameState.instance.itemDrop.CreateRessourceStack()
            //return false;
        }
        else
        {
            //return true;
        }

    }

    public class State
    {
        public Task orderedTask;
        public Task decidedTask;

        public enum StateType
        {
            idle,
            military,
            moving,
            fighting,

        }
    }
    //public Vector2Int FindCaseDropRessources()
    //{

    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          
        
    }
}
