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

    public delegate void ClickOn(ProductionCard _productionCard);

    public ClickOn clickButton;

    public GameObject prodNone;
    public GameObject prodActive;


    public void Click()
    {
        if (clickButton != null)
        {
            clickButton(this);
        }
    }


    public void SetUpCard(Production _production)
    {
        if(_production != null)
        {
            prodActive.SetActive(true);
            prodNone.SetActive(false);
            production = _production;
            progression.SetSlider(0, (int)_production.productionTime);
            result.SetUp(_production);
            cost.SetValues(_production);
        }
        else
        {
            production = null;

            prodActive.SetActive(false);
            prodNone.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActiveProduction&& production!=null)
        {
            building = GameState.instance.controller.selected as Building;
            if(building != null)
            {
                if (building.productionCurrent != null)
                {
                    if (production != building.productionCurrent)
                    {
                        production = building.productionCurrent;
                        SetUpCard(production);
                    }
                    progression.SetSlider((int)building.productionDone, (int)building.productionSpeed);

                }

            }
            
        }
    }
}
