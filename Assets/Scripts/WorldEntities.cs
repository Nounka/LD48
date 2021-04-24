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

    public Task objective;
    public List<Task> actions;

    public float baseSpeed;

    public void AddRessources()
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        /*Map map = GameState.instance.map;
        objective = new MoveTask(map.GetPath(map.GetTile(transform.position), map.GetTile(16, 57)));
        objective.actor = this;*/
    }

    // Update is called once per frame
    void Update()
    {
        /*if (objective != null)
        {
            objective.WorkTask();
        }*/
    }
}
