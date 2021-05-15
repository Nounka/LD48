using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChangeProductionUI : MonoBehaviour
{
    public static ChangeProductionUI instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }


    public GameObject noneProd;
    public GameObject prodPrefab;

    public RectTransform scrollView;
    public RectTransform content;

    public ScrollViewButtonSwitch switchButton;

    public Building currentBuilding;
    public int previousSize;

    public float cardSize = 32f;

    public float desiredSize;
    public float currentSize;
    public float maxViewSize;
    public float unScrollSpeed;
    public bool opening;

    public List<GameObject> choiceCards;

    public ProductionCard activeProduction;

    public void SwitchState(bool _state)
    {
        if (currentBuilding != null)
        {
            if (_state)
            {
                ResizeViewPort(currentBuilding.possibleProduction.Count);
            }
            else
            {
                desiredSize = 0;
            }
        }

    }

    public void SetBuilding(Building _building)
    {
        currentBuilding = _building;
        ResetProd();
        
    }

    public void ClearCards()
    {
        for(int x= 0; x < choiceCards.Count; x++)
        {
            Destroy(choiceCards[x]);
        }
        choiceCards.Clear();
    }

    public void SetAllProduction()//Creer les cartes pour choisir de changer de production
    {
        List<RectTransform> transforms = new List<RectTransform>();
        int position = 0;
        if (currentBuilding.productionCurrent != null) //Cas batiment a une production 
        {
            GameObject obj = Instantiate(prodPrefab, content);
            RectTransform transform = obj.GetComponent<RectTransform>();

            transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, 0);
            position++;
            ProductionCard card = obj.GetComponent<ProductionCard>();
            card.SetUpCard(null);
            card.clickButton += SwitchToProd;
            choiceCards.Add(obj);
        }
        for (int x = 0; x < currentBuilding.possibleProduction.Count; x++)
        {

                if (currentBuilding.productionCurrent != currentBuilding.possibleProduction[x])
                {
                    GameObject obj = Instantiate(prodPrefab, content);
                    RectTransform transform = obj.GetComponent<RectTransform>();
                    transform.anchoredPosition = new Vector2(transform.anchoredPosition.x, position * -cardSize);
                    ProductionCard card = obj.GetComponent<ProductionCard>();
                    card.SetUpCard(currentBuilding.possibleProduction[x]);
                    card.clickButton += SwitchToProd;
                     choiceCards.Add(obj);
                }
            
        }
    }

    public void SwitchToProd(ProductionCard _card)
    {
        currentBuilding.productionCurrent = _card.production;
        currentBuilding.productionDone = 0;
        activeProduction.SetUpCard(_card.production);
        ResetProd();
    }

    public void ResetProd()//Reset les cartes
    {
        ClearCards();
        SetAllProduction();
        ResizeViewPort(currentBuilding.possibleProduction.Count);
        previousSize = currentBuilding.possibleProduction.Count;
    }

    public void ResizeViewPort(int _number)
    {
        desiredSize = cardSize * _number;
    }
    // Start is called before the first frame update
    void Start()
    {
        switchButton.switchButton += SwitchState;
    }

    public void ChangeSize()
    {
        if (currentSize < desiredSize)
        {
            currentSize += unScrollSpeed * Time.deltaTime;
            if (currentSize > desiredSize)
            {
                currentSize = desiredSize;
            }
        }
        else if(currentSize>desiredSize)
        {
            currentSize -= unScrollSpeed * Time.deltaTime;
            if (currentSize < desiredSize)
            {
                currentSize = desiredSize;
            }
        }

        content.sizeDelta = new Vector2(content.sizeDelta.x, currentSize);
        

        if (currentSize < maxViewSize)
        {
            scrollView.anchoredPosition = new Vector3(scrollView.anchoredPosition.x, -(currentSize / 2), 0);
            scrollView.sizeDelta = new Vector2(content.sizeDelta.x, currentSize);
        }
        else
        {
            scrollView.anchoredPosition = new Vector3(scrollView.anchoredPosition.x, -(maxViewSize/ 2), 0);
            scrollView.sizeDelta = new Vector2(content.sizeDelta.x, maxViewSize);
        }

        

    }
    // Update is called once per frame
    void Update()
    {
        if (currentSize != desiredSize)
        {
            ChangeSize();
        }

        if (currentBuilding != null)
        {
            if (previousSize != currentBuilding.possibleProduction.Count)
            {
                ResetProd();
            }
        }
    }
}
