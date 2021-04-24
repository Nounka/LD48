using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public GameObject BuildPanel,PausePanel,RessourcePanel;
    public Text FoodText, WoodText, StoneText;

    // Start is called before the first frame update
    void Start()
    {
        PausePanel.SetActive(false);
        BuildPanel.SetActive(false);
        RessourcePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        PausePanel.SetActive(GameControl.isGamePaused);
        BuildPanel.SetActive(!GameControl.isGamePaused);
        RessourcePanel.SetActive(!GameControl.isGamePaused);

        FoodText.text = GameState.instance.ressources.foodCount.ToString();
        WoodText.text = GameState.instance.ressources.stoneCount.ToString();
        StoneText.text = GameState.instance.ressources.woodCount.ToString();
    }
}
