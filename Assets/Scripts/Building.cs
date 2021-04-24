using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : WorldStaticObject
{
    public Vector2Int size;

    public Vector2Int position;//La position du coin en bas a gauche

    public float structurePointMax;
    public float structurePointCurrent;

    public int costWood;
    public int costFood;
    public int costStone;

    public float currentProduction;
    public float productionSpeed;

    public int stockWoodCurrent;
    public int stockWoodMax;
    public int stockFoodCurrent;
    public int stockFoodMax;
    public int stockStoneCurrent;
    public int stockStoneMax;

    public int prodWood;
    public int prodFood;
    public int prodStone;

    public Production production;
    public int quantity;

    public Worker[] workers;
    public int workerSize;

    public void Work()
    {
        float ratio = 0;

        foreach(Worker work in workers)
        {
            if (work.IsWorking())
            {
                ratio ++;
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
        int required = prodWood + prodStone + prodFood;
        if (required > 0)
        {
            int totalRessources = Mathf.Min(stockWoodCurrent, prodWood) + Mathf.Min(stockFoodCurrent, prodFood) + Mathf.Min(stockStoneCurrent, prodStone);
            float ratio = totalRessources / required;
            return ratio;
        }
        else
        {
            return 1f;
        }
    }

    public void Produce()
    {
        stockFoodCurrent -= prodFood;
        stockStoneCurrent -= prodStone;
        stockWoodCurrent -= prodWood;
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
