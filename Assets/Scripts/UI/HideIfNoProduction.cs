using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfNoProduction : MonoBehaviour
{
    public GameObject noProd;
    public GameObject prodCard;
    public GameObject prodSlider;
    public GameObject prodSwitch;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Building building = (GameState.instance.controller.selected as Building);

        if (building)
        {
            if (building.possibleProduction.Count>0)
            {
                prodSlider.SetActive(true);
                prodSwitch.SetActive(true);
                if (building.productionCurrent != null)
                {
                    noProd.SetActive(false);
                    prodCard.SetActive(true);
                }
                else
                {
                    noProd.SetActive(true);
                    prodCard.SetActive(false);
                }
            }
            else
            {
                noProd.SetActive(false);
                prodCard.SetActive(false);
                prodSlider.SetActive(false);
                prodSwitch.SetActive(false);
            }
        }
    }
}
