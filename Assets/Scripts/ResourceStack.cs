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

    public float Divide(ResourceStack other)
    {
        float res = 10000000000;
        if(other.woodCount>0) 
            res = Mathf.Min(res, woodCount / other.woodCount);
        if(other.foodCount>0) 
            res = Mathf.Min(res, foodCount / other.foodCount);
        if(other.stoneCount>0) 
            res = Mathf.Min(res, stoneCount / other.stoneCount);
        return res;
    }

    public void Substract(ResourceStack other)
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

    // returns the remaining resources
    public ResourceStack addWithinCapacity(ResourceStack other, int capacity)
    {
        int currentSize = GetSize();
        if(currentSize >= capacity)
            return other;
        // here maybe change priority ?
        if(currentSize + other.woodCount > capacity)
        {
            woodCount += capacity-currentSize;
            other.woodCount -= capacity-currentSize;
            return other;
        }
        else
        {
            woodCount += other.woodCount;
            currentSize -= other.woodCount;
            other.woodCount = 0;
        }
        if(currentSize + other.stoneCount > capacity)
        {
            stoneCount += capacity-currentSize;
            other.stoneCount -= capacity-currentSize;
            return other;
        }
        else
        {
            stoneCount += other.stoneCount;
            currentSize -= other.stoneCount;
            other.stoneCount = 0;
        }
        if(currentSize + other.foodCount > capacity)
        {
            foodCount += capacity-currentSize;
            other.foodCount -= capacity-currentSize;
            return other;
        }
        else
        {
            foodCount += other.foodCount;
            currentSize -= other.foodCount;
            other.foodCount = 0;
        }
        return other;
    }
}
