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

    public float productionTime;

}

public class Building : WorldStaticObject
{
    public enum BuildingType
    {
        reproduction,
        entrepot,
        ruins,
        wall,
        workshop,
        bridge
    }

    public BuildingStats patron;
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
        patron = _stats;
        spriteRenderer.sprite = _stats.building_sprite;
        if (construction == null)
        {
            construction = new Construction();
        }
        construction.stockRequired = _stats.buildCost.Copy();
        construction.stockCurrent = new ResourceStack(0, 0, 0);
        construction.needRessource = true;

        if (currentStock == null)
        {
            currentStock = new ResourceStack(0, 0, 0);
        }
        maxStock = _stats.stock.Copy();
        workers = new Worker[_stats.workerRequired];
        for (int x = 0; x < workers.Length; x++)
        {
            if (workers[x] == null)
            {
                workers[x] = new Worker();
            }
        }
        workerSize = _stats.workerRequired;
        structurePointCurrent = 1;
        GameState.instance.buildingsOnMap.Add(this);
        isActive = true;
    }

    public void SetProduction()
    {
        if (possibleProduction == null)
        {
            possibleProduction = new List<Production>();

        }
        possibleProduction = GameState.instance.unlocks.GetProductionAvailable(patron.type);
        if (possibleProduction.Count > 0)
        {
            if (possibleProduction.Count == 1)
            {
                SetToProduction(possibleProduction[0]);
            }
            else
            {

            }

        }


    }

    public void SetToProduction(Production _production)
    {
        productionCurrent = _production;
        productionSpeed = _production.productionTime;
        productionDone = 0;
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
        Destroy(gameObject);
    }

    public void Work()
    {
        float ratio = 0;

        foreach (Worker work in workers)
        {
            if (work != null)
            {
                if (work.IsWorking())
                {
                    ratio++;
                }
            }

        }

        ratio = ratio / workerSize;
        productionDone += ratio * Time.deltaTime;
        if (productionDone / productionSpeed >= MaxProdRatio())
        {
            productionDone = productionSpeed * MaxProdRatio();
        }
        if (productionDone >= productionSpeed)
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
            for (int x = 0; x < productionCurrent.citizenNumber; x++)
            {
                Citizen cit = GameState.instance.citizenGenerator.CreateCitizen(productionCase);
                cit.positionCase = productionCase;
                cit.MoveTo(new Vector2(productionCase.x + 0.5f, productionCase.y));
            }

            currentStock.woodCount -= prod.woodCount;
            currentStock.foodCount -= prod.foodCount;
            currentStock.stoneCount -= prod.stoneCount;
            productionDone = 0;
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

    public void WorkOnBuilding(float _value)
    {
        float previous = construction.workCurrent;
        construction.workCurrent += _value;
        if (construction.workCurrent / patron.buildingTime > construction.RatioDoable())
        {
            construction.workCurrent = construction.RatioDoable() * patron.buildingTime;
            construction.needRessource = true;
        }
        float builded = construction.workCurrent - previous;
        structurePointCurrent += patron.structureFinal * (builded/patron.buildingTime);
        structurePointCurrent = Mathf.Min(patron.structureFinal, structurePointCurrent);

        // If the building phase is
        if (construction.workCurrent >= patron.buildingTime)
        {
            
            spriteRenderer.sprite = patron.sprite;
            structurePointCurrent = Mathf.Min(patron.structureFinal, structurePointCurrent);
            isConstructing = false;
        }
    }

    public bool TryGetInside(Citizen _citizen)
    {
        if (GetSpace() < workers.Length)
        {
            bool gotInside = false;
            for (int x = 0; x < workers.Length; x++)
            {
                if (!gotInside)
                {
                    if (workers[x] == null)
                    {
                        workers[x] = new Worker(_citizen);
                        gotInside = true;
                    }
                    if (workers[x].citizen == null)
                    {
                        workers[x].citizen = _citizen;
                        gotInside = true;

                    }
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public int GetSpace()
    {
        int max = workers.Length;
        foreach (Worker work in workers)
        {
            if (work.citizen == null)
            {
                max--;
            }
        }
        return max;
    }

    public void Stock(ResourceStack _drop)
    {
        GameState.instance.ressources.Add(_drop);
    }

    [System.Serializable]
    public class Worker
    {
        public Citizen citizen;
        public bool IsWorking()
        {
            if (citizen == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public Worker(Citizen _citizen)
        {
            citizen = _citizen;
        }
        public Worker()
        {

        }
    }

    [System.Serializable]
    public class Construction
    {
        public ResourceStack stockCurrent;
        public ResourceStack stockRequired;
        public float workCurrent;
        public bool needRessource;

        public float RatioDoable()
        {
            int required = stockRequired.GetSize();
            int totalRessource = Mathf.Min(stockCurrent.woodCount, stockRequired.woodCount) + Mathf.Min(stockCurrent.stoneCount, stockRequired.stoneCount) + Mathf.Min(stockCurrent.foodCount, stockRequired.foodCount);
            float ratio = totalRessource / required;
            return ratio;
        }

        public void AddRessource(ResourceStack _ajout)
        {
            stockCurrent.Add(_ajout);
        }
    }

    public void TakeRessources()
    {
        if (isConstructing)
        {
            if (construction.needRessource)
            {
                if (construction.stockCurrent.woodCount < construction.stockRequired.woodCount)
                {
                    int woodrequired = construction.stockRequired.woodCount - construction.stockCurrent.woodCount;
                    if (GameState.instance.ressources.woodCount > 0)
                    {
                        int take = Mathf.Min(woodrequired, GameState.instance.ressources.woodCount);
                        construction.stockCurrent.woodCount += take;
                        GameState.instance.ressources.woodCount -= take;
                    }
                }
                if (construction.stockCurrent.foodCount < construction.stockRequired.foodCount)
                {
                    int foodrequired = construction.stockRequired.foodCount - construction.stockCurrent.foodCount;
                    if (GameState.instance.ressources.foodCount > 0)
                    {
                        int take = Mathf.Min(foodrequired, GameState.instance.ressources.foodCount);
                        construction.stockCurrent.foodCount += take;
                        GameState.instance.ressources.foodCount -= take;
                    }
                }
                if (construction.stockCurrent.stoneCount < construction.stockRequired.stoneCount)
                {
                    int stonerequired = construction.stockRequired.stoneCount - construction.stockCurrent.stoneCount;
                    if (GameState.instance.ressources.stoneCount > 0)
                    {
                        int take = Mathf.Min(stonerequired, GameState.instance.ressources.stoneCount);
                        construction.stockCurrent.stoneCount += take;
                        GameState.instance.ressources.stoneCount -= take;
                    }
                }
                if (construction.stockCurrent.woodCount >= construction.stockRequired.woodCount && construction.stockCurrent.foodCount >= construction.stockRequired.foodCount && construction.stockCurrent.stoneCount >= construction.stockRequired.stoneCount)
                {//A toute les ressources
                    construction.needRessource = false;
                }
            }
            else
            {
                if (isActive)
                {
                    if (currentStock.woodCount < maxStock.woodCount)
                    {
                        int woodrequired = maxStock.woodCount - currentStock.woodCount;
                        if (GameState.instance.ressources.woodCount > 0)
                        {
                            int take = Mathf.Min(woodrequired, GameState.instance.ressources.woodCount);
                            currentStock.woodCount += take;
                            GameState.instance.ressources.woodCount -= take;
                        }
                    }
                    if (currentStock.foodCount < maxStock.foodCount)
                    {
                        int foodrequired = maxStock.foodCount - currentStock.foodCount;
                        if (GameState.instance.ressources.foodCount > 0)
                        {
                            int take = Mathf.Min(foodrequired, GameState.instance.ressources.foodCount);
                            currentStock.foodCount += take;
                            GameState.instance.ressources.foodCount -= take;
                        }
                    }
                    if (currentStock.stoneCount < maxStock.stoneCount)
                    {
                        int stonerequired = maxStock.stoneCount - currentStock.stoneCount;
                        if (GameState.instance.ressources.stoneCount > 0)
                        {
                            int take = Mathf.Min(stonerequired, GameState.instance.ressources.stoneCount);
                            currentStock.stoneCount += take;
                            GameState.instance.ressources.stoneCount -= take;
                        }
                    }
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (structurePointCurrent < 0)
        {
            Crumble();
        }
        if (!isConstructing && isActive && productionCurrent != null)
        {
            Work();
        }
        if (isActive)
        {
            TakeRessources();
        }
    }
}
