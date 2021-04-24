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



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
          
        
    }
}
