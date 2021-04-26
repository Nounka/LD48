using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideIfNoProduction : MonoBehaviour
{
    public GameObject toHide;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Building building = (GameState.instance.controller.selected as Building);
        if (building && !building.isConstructing && building.productionCurrent != null && building.productionCurrent.tool != null) {
            // Display
            toHide.SetActive(true);
        } else {
            // Hide
            toHide.SetActive(false);
        }
    }
}
