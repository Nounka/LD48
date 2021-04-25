using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject BuildDescriptionPanel, BuildPanel, PausePanel, RessourcePanel, SelectedBuildingPanel, SelectedWorkerPanel;
    public Text BuildDescriptionText;

    private GameState gameState;
    private Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        SelectedWorkerPanel.SetActive(false);
        SelectedBuildingPanel.SetActive(false);
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
        SelectedWorkerPanel.SetActive(controller.mode == Controller.ControlerMode.selectUnit);
        SelectedBuildingPanel.SetActive(controller.mode == Controller.ControlerMode.selectBuilding);
        bool isBuildMode = (controller.mode == Controller.ControlerMode.placeBuilding);
        BuildDescriptionPanel.SetActive(isBuildMode);
        if (isBuildMode)
        {
            string text = "Missing Object Data !";
            try
            {
                text = controller.ghostBuilding.currentStats.ToString();
            }
            catch (System.Exception e)
            {
                // blah !
            }
            BuildDescriptionText.text = text;
        }
    }
}
