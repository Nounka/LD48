using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ResourceGatherStats
{
    public ResourceType type;
    public float speed;
    public int quantity;
}

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
    public ItemDropManager itemDropManager;
    public TaskManager taskManager;
    public SchematicUnlock unlocks;
    public Controller controller;
    public AudioBank audioBank;
    public Camera cam;


    public float combatSpeed;
    public float buildSpeed;
    public float gatherSpeed;
    public float dropSpeed;
    public int carryCapacity;
    public float attackSpeed;
    public float allDommage;//The value of dommage that all do
    public List<ResourceGatherStats> baseRessourcesStats;

    public Vector2Int ennemySize;

    public List<Building> buildingsOnMap;
    public List<Citizen> citizens;
    public List<Ennemy> ennemys;

    public Building ruin;

    public OverMind overMind;

    public AudioMixer mixer;

    public SelectorIndicator selectIndicator;

    public NameManager nameManager;

    public static List<Vector2Int> neighboursVectorD = new List<Vector2Int> { new Vector2Int(1, 0),new Vector2Int(1,1),new Vector2Int(1,-1),new Vector2Int(0,1),new Vector2Int(0,-1),new Vector2Int(-1,0),new Vector2Int(-1,1),new Vector2Int(-1,-1) };

    public static List<Vector2Int> neighbours2VectorD = new List<Vector2Int> { new Vector2Int(1, 0), new Vector2Int(1, 1), new Vector2Int(1, -1), new Vector2Int(0, 1), new Vector2Int(0, -1), new Vector2Int(-1, 0), new Vector2Int(-1, 1),
        new Vector2Int(2,2),new Vector2Int(2,1 ),new Vector2Int(2,0 ),new Vector2Int(2, -1),new Vector2Int(2,-2 ),new Vector2Int(1,-2 ),new Vector2Int(0,-2 ),new Vector2Int(-1,-2 ),new Vector2Int(-2,-2 ),
    new Vector2Int(-2,-1 ),new Vector2Int(-2,0),new Vector2Int(-2,1),new Vector2Int(-2,2),new Vector2Int(-1,2),new Vector2Int(0,2),new Vector2Int(1,2),new Vector2Int(2,2)};


    public ResourceGatherStats GetGatherStats(ResourceNodes _target) // Renvoie les stats de base pour recolter une ressource
    {
        foreach(ResourceGatherStats stats in baseRessourcesStats)
        {
            if (stats.type == _target.type)
            {
                return stats;
            }
        }
        return null;
    }

    public ResourceGatherStats GetGatherStats(ResourceNodes _target,Tool _tool) //Renvoie la stat modifier par l'outil
    {
        ResourceGatherStats baseStats = new ResourceGatherStats();
        baseStats.type = _target.type;
        foreach(ResourceGatherStats stats in baseRessourcesStats)
        {
            if(stats.type == _target.type)
            {
                baseStats.quantity = stats.quantity;
            }
        }
        baseStats.quantity += _tool.stats.force;
        baseStats.speed = _tool.stats.speedModifier;

        return baseStats;
    }

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

    public void CheckDefeat()
    {
        if (ruin == null)
        {
            GameControl.state = GameStateEnum.inGameDefeat;
        }
        if (citizens.Count < 0)
        {
            GameControl.state = GameStateEnum.inGameDefeat;
        }
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
        if (overMind == null)
        {
            overMind = new OverMind();
        }           
    }

    public float checkSpeed;
    float checkTimer;

    // Update is called once per frame
    void Update()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer > checkSpeed)
        {
            CheckDefeat();
        }
    }
}
