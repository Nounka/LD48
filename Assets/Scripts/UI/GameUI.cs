using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    // Singleton
    public static GameUI instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject BuildDescriptionPanel, BuildPanel, PausePanel, RessourcePanel, SelectedEnemyPanel, SelectedBuildingPanel, SelectedWorkerPanel;
    public Text BuildDescriptionText;

    private GameState gameState;
    private Controller controller;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.instance;
        controller = GameState.instance.controller;

        SelectedEnemyPanel.SetActive(false);
        SelectedWorkerPanel.SetActive(false);
        SelectedBuildingPanel.SetActive(false);
        PausePanel.SetActive(false);
        BuildDescriptionPanel.SetActive(false);
        BuildPanel.SetActive(true);
        RessourcePanel.SetActive(true);
    }

    public void ControllerUpdateState(Controller.ControlerMode newState) {
        SelectedEnemyPanel.SetActive(controller.mode == Controller.ControlerMode.selectEnnemy);
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

    // Update is called once per frame
    void Update()
    {
        PausePanel.SetActive(GameControl.isGamePaused);
    }
}
