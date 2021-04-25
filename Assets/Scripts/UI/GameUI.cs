using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject BuildPanel,PausePanel,RessourcePanel,SelectedEntityPanel;
    public Text FoodText, WoodText, StoneText;

    private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        SelectedEntityPanel.SetActive(false);
        PausePanel.SetActive(false);
        BuildPanel.SetActive(true);
        RessourcePanel.SetActive(true);
        gameState = GameState.instance;
    }

    // Update is called once per frame
    void Update()
    {
        PausePanel.SetActive(GameControl.isGamePaused);
        bool isUnitSelected = (gameState.selection == GameState.SelectionState.UnitSelected || gameState.selection == GameState.SelectionState.BuildingSelected);
        SelectedEntityPanel.SetActive(isUnitSelected);

        FoodText.text = gameState.ressources.foodCount.ToString();
        WoodText.text = gameState.ressources.stoneCount.ToString();
        StoneText.text = gameState.ressources.woodCount.ToString();
    }
}
