using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingButton : MonoBehaviour
{
    // Replace this with the proper object type
    public BuildingStats buildingData;
    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.instance;
        gameObject.GetComponent<Button>().onClick.AddListener(() => {
            gameState.controller.EnterBuildingMode(buildingData);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
