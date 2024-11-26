using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public abstract class Harvestable
{
    protected Resource[] harvestableItems;
    protected Vector3 harvestablePosition;
    public abstract Resource[] harvest();

    public Vector3 getHarvestablePosition()
    {
        return harvestablePosition;
    }
}