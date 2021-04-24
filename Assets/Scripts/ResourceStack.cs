using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    WOOD,
    STONE,
    FOOD
};

// can be used to represent inventory or cost
public class ResourceStack : MonoBehaviour
{
    public int woodCount;
    public int foodCount;
    public int stoneCount;

    public ResourceStack(int wood, int food, int stone)
    {
        woodCount = wood;
        foodCount = food;
        stoneCount = stone;
    }

    public ResourceStack(ResourceType type, int quantity)
    {
        woodCount = foodCount = stoneCount = 0;
        switch(type)
        {
            case ResourceType.WOOD:
                woodCount = quantity;
            break;
            case ResourceType.FOOD:
                foodCount = quantity;
            break;
            case ResourceType.STONE:
                stoneCount = quantity;
            break;
        }
    }

    public bool isGreaterThan(ResourceStack other)
    {
        return (woodCount >= other.woodCount && foodCount >= other.foodCount && stoneCount >= other.stoneCount);
    }

    public void Remove(ResourceStack other)
    {
        woodCount -= other.woodCount;
        foodCount -= other.foodCount;
        stoneCount -= other.stoneCount;
    }

    public void Add(ResourceStack other)
    {
        woodCount += other.woodCount;
        foodCount += other.foodCount;
        stoneCount += other.stoneCount;
    }

    public int GetSize()
    {
        return woodCount+foodCount+stoneCount;
    }
}
