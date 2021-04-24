using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : WorldStaticObject
{

    public float structurePointMax;
    public float structurePointCurrent;

    public ResourceStack cost;

    public float currentProduction;
    public float productionSpeed;

    public ResourceStack currentStock;
    public ResourceStack maxStock;

    public ResourceStack prod;

    public Production production;
    public int quantity;

    public Worker[] workers;
    public int workerSize;

    public bool isConstructing;
    public Construction construction;

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
        currentProduction += ratio * Time.deltaTime;
        if (currentProduction / productionSpeed > MaxProdRatio())
        {
            currentProduction = productionSpeed * MaxProdRatio();
        }
        if (currentProduction > productionSpeed)
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
        currentProduction -= productionSpeed;
    }

    public class Production
    {

    }

    public class Worker
    {
        public bool IsWorking()
        {
            return false;
        }
    }

    public class Construction
    {
        public float structurePointWhenBuild;
        public float structurePointConstruction;


        public ResourceStack stockCurrent;
        public ResourceStack stockRequired;

        public float workRequired;
        public float workCurrent;
        public bool needRessource;


        public float RatioDoable()
        {
            int required = stockRequired.GetSize();
            int totalRessource = Mathf.Min(stockCurrent.woodCount, stockRequired.woodCount) + Mathf.Min(stockCurrent.stoneCount, stockRequired.stoneCount) + Mathf.Min(stockCurrent.foodCount, stockRequired.foodCount);
            float ratio = totalRessource / required;
            return ratio;
        }

        public void AddWork(float _value)
        {
            workCurrent += -_value;
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
        Work();   
    }
}
