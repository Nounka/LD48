using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RessourceHandlerEnum
{
    buildingStock,
    unitStock,
    productionCost,
    globalRessources,

}

public class RessourceHandler
{
    public delegate ResourceStack getRessources();
    public getRessources getStack;

    public static RessourceHandler getRessourceHandler(RessourceHandlerEnum type)
    {
        switch (type)
        {
            case RessourceHandlerEnum.buildingStock:
                return buildingStock;
            case RessourceHandlerEnum.unitStock:
                return unitStock;
            case RessourceHandlerEnum.productionCost:
                return productionCost;
            case RessourceHandlerEnum.globalRessources:
            default:
                return globalRessources;
        }
    }

    private RessourceHandler(getRessources internalFunction)
    {
        this.getStack = internalFunction;
    }

    private static RessourceHandler buildingStock = new RessourceHandler(() => (GameState.instance.controller.selected as Building).currentStock);
    private static RessourceHandler unitStock = new RessourceHandler(() => (GameState.instance.controller.selected as WorldEntities).carrying);
    private static RessourceHandler productionCost = new RessourceHandler(() => (GameState.instance.controller.selected as Building).productionCurrent.cost);
    private static RessourceHandler globalRessources = new RessourceHandler(() => GameState.instance.ressources);

}