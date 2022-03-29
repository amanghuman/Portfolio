using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePlantClass
{
    protected string plantClassName;
    protected string plantClassType;
    private string plantClassDescription;

    protected int secondsInMinute = 60;
    private int fertilizerCost;
    //Number of buckets needed to grow the plant
    private int waterCost;
    //Plant unique Identifier ID
    protected int uniqueID;
    //Time needed to grow
    protected float totalTimeNeededToGrow;
    //Time remaining till fully grown
    protected float timeRemainingToGrow;
    //Time since plant is last watered
    protected float timeSinceWatered;
    //Season the plant can grow
    private string season;

    public string PlantClassName
    {
        get { return plantClassName; }
        set { plantClassName = value; }
    }

    public string PlantClassType
    {
        get { return plantClassType; }
        set { plantClassType = value; }
    }
    public string PlantClassDescription
    {
        get { return plantClassDescription; }
        set { plantClassDescription = value; }
    }
    public int FertilizerCost
    {
        get { return fertilizerCost; }
        set { fertilizerCost = value; }
    }
    public int WaterCost
    {
        get { return waterCost; }
        set { waterCost = value; }
    }
    public int UniqueID
    {
        get { return uniqueID; }
        set { uniqueID = value; }
    }
    public float TotalTimeNeededToGrow
    {
        get { return totalTimeNeededToGrow; }
        set { totalTimeNeededToGrow = value; }
    }
    public float TimeRemainingToGrow
    {
        get { return timeRemainingToGrow; }
        set { timeRemainingToGrow = value; }
    }
    public float TimeSinceWatered
    {
        get { return timeSinceWatered; }
        set { timeSinceWatered = value; }
    }
    public string Season
    {
        get { return season; }
        set { season = value; }
    }
}
