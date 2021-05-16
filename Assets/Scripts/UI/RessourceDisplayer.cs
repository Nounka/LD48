using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RessourceDisplayer : MonoBehaviour
{
    public RessourceHandlerEnum ress;

    private RessourceHandler handler;
    public Text food,wood,stone;

    // Start is called before the first frame update
    void Start()
    {
        if (food == null)
        {
            food = GetComponentsInChildren<Text>()[0];
        }
        if (wood == null)
        {
            wood = GetComponentsInChildren<Text>()[1];
        }
        if (stone == null)
        {
            stone = GetComponentsInChildren<Text>()[2];
        }

        handler = RessourceHandler.getRessourceHandler(ress);
    }

    // Update is called once per frame
    void Update()
    {
        ResourceStack stack = handler.getStack();
        food.text = stack.foodCount.ToString();
        wood.text = stack.woodCount.ToString();
        stone.text = stack.stoneCount.ToString();
    }
}
