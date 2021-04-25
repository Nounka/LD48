using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameState : MonoBehaviour
{
    // Singleton
    public static GameState instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Map map;
    public ResourceStack ressources;
    public CitizenGenerator citizenGenerator;
    public ItemDrop itemDrop;
    public TaskManager taskManager;
    public SchematicUnlock unlocks;
    public Controller controller;
    public AudioBank audioBank;

    public float combatSpeed;
    public float buildSpeed;
    public float gatherSpeed;

    public static List<Vector2Int> neighboursVectorD = new List<Vector2Int> { new Vector2Int(1, 0),new Vector2Int(1,1),new Vector2Int(1,-1),new Vector2Int(0,1),new Vector2Int(0,-1),new Vector2Int(-1,0),new Vector2Int(-1,1),new Vector2Int(-1,-1) };

    public void EntityDie(WorldEntities _entity)
    {

    }

    public void BuildingCrumble(Building _building)
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
