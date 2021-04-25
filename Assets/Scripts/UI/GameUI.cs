using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject BuildDescriptionPanel,BuildPanel,PausePanel,RessourcePanel,SelectedEntityPanel;
    public Text FoodText, WoodText, StoneText;

    private GameState gameState;
    private Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        SelectedEntityPanel.SetActive(false);
        PausePanel.SetActive(false);
        BuildDescriptionPanel.SetActive(false);
        BuildPanel.SetActive(true);
        RessourcePanel.SetActive(true);
        gameState = GameState.instance;
        controller = GameState.instance.controller;
    }

    // Update is called once per frame
    void Update()
    {
        PausePanel.SetActive(GameControl.isGamePaused);
        bool isUnitSelected = (controller.mode == Controller.ControlerMode.selectUnit || controller.mode == Controller.ControlerMode.selectBuilding);
        SelectedEntityPanel.SetActive(isUnitSelected);
        bool isBuildMode = (controller.mode == Controller.ControlerMode.placeBuilding);
        BuildDescriptionPanel.SetActive(isBuildMode);
        if (isBuildMode) {
            BuildDescriptionPanel.GetComponent<Text>().text = controller.ghostBuilding.currentStats.ToString();
        }

        FoodText.text = gameState.ressources.foodCount.ToString();
        WoodText.text = gameState.ressources.stoneCount.ToString();
        StoneText.text = gameState.ressources.woodCount.ToString();
    }
}
