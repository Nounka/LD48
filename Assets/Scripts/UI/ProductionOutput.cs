using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductionOutput : MonoBehaviour
{
    public Text Count;
    public Image Renderer;

    // Start is called before the first frame update
    void Start()
    {
        Count.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        Building building = (GameState.instance.controller.selected as Building);
        if ( building.productionCurrent != null && building.productionCurrent.tool != null ) {
            Count.text = building.productionCurrent.toolQuantity.ToString();
            Renderer.sprite = building.productionCurrent.tool.sprite;
        }
    }
}
