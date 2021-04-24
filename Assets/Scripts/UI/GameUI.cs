using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject BuildPanel,PausePanel,RessourcePanel;

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
    }
}
