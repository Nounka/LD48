using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourcesDisplayerWeak : MonoBehaviour
{
    private Text food, wood, stone;

    public void SetValues(Production _prod)
    {
        ResourceStack stack = _prod.cost;
        food.text = stack.foodCount.ToString();
        wood.text = stack.woodCount.ToString();
        stone.text = stack.stoneCount.ToString();
    }


    // Start is called before the first frame update
    void Start()
    {
        food = GetComponentsInChildren<Text>()[0];
        wood = GetComponentsInChildren<Text>()[1];
        stone = GetComponentsInChildren<Text>()[2];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
