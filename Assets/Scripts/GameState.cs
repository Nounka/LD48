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
    public int carryCapacity;


    public List<Building> buildingsOnMap;
    public List<Citizen> citizens;
    public List<Ennemy> ennemys;

    public static List<Vector2Int> neighboursVectorD = new List<Vector2Int> { new Vector2Int(1, 0),new Vector2Int(1,1),new Vector2Int(1,-1),new Vector2Int(0,1),new Vector2Int(0,-1),new Vector2Int(-1,0),new Vector2Int(-1,1),new Vector2Int(-1,-1) };

    public void EntityDie(WorldEntities _entity)
    {
        if (_entity.isCitizen)
        {
            Citizen cit = (Citizen)_entity;
            citizens.Remove(cit);
        }
        else
        {
            Ennemy ene = (Ennemy)_entity;
            ennemys.Remove(ene);
        }
    }

    public void BuildingCrumble(Building _building)
    {
        buildingsOnMap.Remove(_building);
    }

    public List<Citizen> GetCitizenBellow(int _y)
    {
        List<Citizen> retour = new List<Citizen>();
        foreach(Citizen cit in citizens)
        {
            if (cit.position.y < _y)
            {
                if (!cit.insideBuilding)
                {
                    retour.Add(cit);
                }
                
            }
        }
        return retour;
    }
    public List<Building> GetBuildingBellow(int _y)
    {
        List<Building> retour = new List<Building>();
        foreach (Building build in buildingsOnMap)
        {
            if (build.position.y < _y)
            {
                retour.Add(build);
            }
        }
        return retour;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (buildingsOnMap == null)
        {
            buildingsOnMap = new List<Building>();
        }
        if(citizens == null)
        {
            citizens = new List<Citizen>();
        }
        if(ennemys == null)
        {
            ennemys = new List<Ennemy>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
