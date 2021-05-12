using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionCard : MonoBehaviour
{
    public Building building;
    public Production production;

    public ProductionOutput result;
    public ProgressBar progression;
    public RessourcesDisplayerWeak cost;

    public bool IsActiveProduction;

    public void CLick()
    {
        if (!IsActiveProduction)
        {
            building.SetToProduction(production);
        }
        
    }

    public void SetUpCard(Production _production)
    {
        production = _production;
        progression.SetSlider(0,(int)_production.productionTime);
        result.SetUp(_production);
        cost.SetValues(_production);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActiveProduction)
        {
            building = GameState.instance.controller.selected as Building;
            if(building != null)
            {
                if (building.productionCurrent != null)
                {
                    progression.SetSlider((int)building.productionDone, (int)building.productionSpeed);
                    if (production != building.productionCurrent)
                    {
                        production = building.productionCurrent;
                        SetUpCard(production);
                    }
                }

            }
            
        }
    }
}
