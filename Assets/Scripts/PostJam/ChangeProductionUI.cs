using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeProductionUI : MonoBehaviour
{
    public GameObject noneProd;
    public GameObject prodPrefab;

    public RectTransform scrollView;
    public RectTransform content;

    public ScrollViewButtonSwitch switchButton;

    public Building currentBuilding;
    public int previousSize;

    public List<GameObject> cards;

    public void SwitchState(bool _state)
    {
        if (_state)
        {
            gameObject.SetActive(true);
            ResizeViewPort(currentBuilding.possibleProduction.Count);

        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void SetAllProduction()//Creer les cartes pour choisir de changer de production
    {
        List<Transform> transforms = new List<Transform>();
        for(int x = 0; x < currentBuilding.possibleProduction.Count; x++)
        {
            if (currentBuilding.productionCurrent != null) //Cas batiment a une production 
            {
                GameObject obj = Instantiate(noneProd,content);
            }
            else
            {
                if (currentBuilding.productionCurrent != currentBuilding.possibleProduction[x])
                {

                }
            }
        }
    }

    public void ResizeViewPort(int _number)
    {
        content.sizeDelta = new Vector2(content.sizeDelta.x, 32 * _number);
    }
    // Start is called before the first frame update
    void Start()
    {
        switchButton.switchButton += SwitchState;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBuilding != null)
        {
            if (previousSize != currentBuilding.possibleProduction.Count)
            {
                for(int x = 0; x < cards.Count;x++)
                {
                    Destroy(cards[x]);
                }
                cards.Clear();
                SetAllProduction();
                ResizeViewPort(currentBuilding.possibleProduction.Count);
                previousSize = currentBuilding.possibleProduction.Count;
            }
        }
    }
}
