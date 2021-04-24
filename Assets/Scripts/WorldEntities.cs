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

    public enum TaskType
    {
        move,
        attack,
        build,
    }
    public class Task
    {
        public string type;
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
