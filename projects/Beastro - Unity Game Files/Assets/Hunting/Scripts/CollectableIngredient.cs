using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableIngredient : Interactive
{
    // Reference to gameobject so that it can be destroyed
    private GameObject thisObject;

    // Iteration 3
    [SerializeField] private string inventoryItemName;
    [SerializeField] private int amountPerDrop;

    private void Start()
    {
        thisObject = transform.gameObject;
    }

    protected override void OnInteract()
    {
        Inventory.Add(inventoryItemName, amountPerDrop);    // Iteration 3
        Debug.Log("Add To Inventory: " + amountPerDrop + " " + inventoryItemName);
        Destroy(thisObject);
    }

    private void OnDestroy()
    {
        ManualRemove();
    }
}
