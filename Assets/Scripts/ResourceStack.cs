using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// can be used to represent inventory or cost
public class ResourceStack : MonoBehaviour
{
    public int woodCount;
    public int foodCount;
    public int stoneCount;

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
}
