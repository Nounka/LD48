using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Production
{
    public ToolStats tool;
    public int quantity;

    public int citizenNumber;

    public ResourceStack cost;

}

public class Building : WorldStaticObject
{
    public enum BuildingType
    {
        reproduction,
        entrepot,
        ruins,

    }

    public BuildingType type;

    public SpriteRenderer spriteRenderer;

    public float structurePointMax;
    public float structurePointCurrent;

    public float productionDone;
    public float productionSpeed;

    public ResourceStack currentStock;
    public ResourceStack maxStock;

    public ResourceStack prod;

    public Production productionCurrent;
    public List<Production> possibleProduction;
    public int quantity;

    public Worker[] workers;
    public int workerSize;

    public bool isActive;
    public bool isConstructing;
    public Construction construction;

    public Vector2Int productionCase;
    public Vector2Int entrance;

    public void SetUp(BuildingStats _stats)
    {
        isConstructing = true;
        if(construction == null)
        {
            construction = new Construction();
        }
        construction.stockRequired = new ResourceStack(_stats.buildCost.woodCount, _stats.buildCost.foodCount,_stats.buildCost.foodCount);
        construction.structurePointWhenBuild = _stats.structureFinal;

        if (currentStock == null)
        {
            currentStock = new ResourceStack(0, 0, 0);
        }
        maxStock = _stats.stock.Copy();
        workers = new Worker[_stats.workerRequired];
        workerSize = _stats.workerRequired;
        structurePointMax = _stats.structureConstruction;
        structurePointCurrent = structurePointMax;
    }

    public void SetProduction()
    {
       if(possibleProduction == null)
        {
            possibleProduction = new List<Production>();

        }
        possibleProduction = GameState.instance.unlocks.GetProductionAvailable(type);
        if (possibleProduction.Count > 0)
        {
            if (possibleProduction.Count == 1)
            {
                productionCurrent = possibleProduction[0];
            }
            else 
            {
                
            }

        }


    }
    public void SetValueConstruction(BuildingStats _stats)
    {

    }

    public void TakeDommage(float _dommage)
    {
        structurePointCurrent -= _dommage;
    }

    public void Crumble()
    {
        GameState.instance.BuildingCrumble(this);
    }

    public void Work()
    {
        float ratio = 0;

        foreach (Worker work in workers)
        {
            if (work.IsWorking())
            {
                ratio++;
            }
        }

        ratio = ratio / workerSize;
        productionDone += ratio * Time.deltaTime;
        if (productionDone / productionSpeed > MaxProdRatio())
        {
            productionDone = productionSpeed * MaxProdRatio();
        }
        if (productionDone > productionSpeed)
        {
            Produce();
        }
    }

    public float MaxProdRatio()
    {
        int required = prod.GetSize();
        if (required > 0)
        {
            int totalRessource = Mathf.Min(currentStock.woodCount, prod.woodCount) + Mathf.Min(currentStock.stoneCount, prod.stoneCount) + Mathf.Min(currentStock.foodCount, prod.foodCount);
            float ratio = totalRessource / required;
            return ratio;
        }
        else
        {
            return 1f;
        }
    }

    public void Produce()
    {
        currentStock.Substract(prod);
        productionDone -= productionSpeed;

        if (productionCurrent.citizenNumber > 0)
        {
            for(int x = 0; x < productionCurrent.citizenNumber; x++)
            {
                Citizen cit = GameState.instance.citizenGenerator.CreateCitizen();
                cit.positionCase = productionCase;
                cit.MoveTo(new Vector2(productionCase.x + 0.5f, productionCase.y));
            }
            
        }
        if (productionCurrent.tool != null)
        {

        }
    }

    public void GetOutside(Worker _work)
    {
        _work.citizen.GetOutside();
        _work.citizen.insideBuilding = null;
        _work = null;
    }

  public void Construct()
    {
        spriteRenderer.sprite = construction.finishSprite;
        isConstructing = false;
    }
    public class Worker
    {
        public Citizen citizen;
        public bool IsWorking()
        {
            return false;
        }
    }

    public class Construction
    {
        public float structurePointWhenBuild;

        public ResourceStack stockCurrent;
        public ResourceStack stockRequired;

        public float workRequired;
        public float workCurrent;
        public bool needRessource;
        public Sprite finishSprite;

        public float RatioDoable()
        {
            int required = stockRequired.GetSize();
            int totalRessource = Mathf.Min(stockCurrent.woodCount, stockRequired.woodCount) + Mathf.Min(stockCurrent.stoneCount, stockRequired.stoneCount) + Mathf.Min(stockCurrent.foodCount, stockRequired.foodCount);
            float ratio = totalRessource / required;
            return ratio;
        }

        public void AddWork(float _value)
        {
            workCurrent += _value;
            if (workCurrent / workRequired > RatioDoable())
            {
                workCurrent = RatioDoable() * workRequired;
                needRessource = true;
            }
        }

        public void AddRessource(ResourceStack _ajout)
        {

        }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (structurePointCurrent < 0)
        {
            Crumble();
        }
        if (!isConstructing && isActive&&productionCurrent!=null)
        {
            Work();
        }
        
    }
}
