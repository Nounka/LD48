using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductionCard : MonoBehaviour
{
    public Building building;
    public Production production;

    public GameObject result;
    public GameObject progression;
    public GameObject Cost;

    public bool IsActiveProduction;

    public void CLick()
    {
        if (!IsActiveProduction)
        {
            building.SetToProduction(production);
        }
        
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
