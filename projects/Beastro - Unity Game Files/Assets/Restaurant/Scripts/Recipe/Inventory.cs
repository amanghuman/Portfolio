using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public struct inventoryCount
    {//relates an inventory item to
        public string item;
        public int amount;
    }
    public static List<inventoryCount> inventoryCounts = new List<inventoryCount>();

    void Start()
    {//only non-static, only for testing intiialization
        //set up a bunch values for ingedients, so it can be seen working
        Add("bread", 100);
        Add("beef", 100);
        Add("chicken", 100);
        Add("salt", 100);
        Add("pepper", 100);
        Add("carrotSeeds", 100);
        Add("onionSeeds", 100);
        Add("cabbageSeeds", 100);
        Add("wheatSeeds", 100);
        Add("fertilizer", 100);
    }
    public static void Add(string addItem, int amount)
    {
        inventoryCount myItem;
        myItem.item = addItem;
        int index = inventoryCounts.FindIndex(ic => ic.item == addItem);//find this item already in list
        if (index == -1)
        {//if this isnt found in the inventory, add it
            myItem.amount = amount;//set the amount for adding
            inventoryCounts.Add(myItem);//add the new item
        }
        else
        {//item already exists in inventory, add amount
            myItem.amount = inventoryCounts[index].amount + amount;//find updated amount
            inventoryCounts[index] = myItem;//overwrite with new amount
        }
    }
    public static int CheckItem(string checkingItem)
    {//return the amount of an itme in the inventory
        int index = inventoryCounts.FindIndex(ic => ic.item == checkingItem);//find item
        if (index == -1)
            return 0;//if its not there return 0
        else
            return inventoryCounts[index].amount;//if it's in the inventory, return the amount
    }
    public static bool CheckRecipe(Recipe recipeChecking)
    {//returns if the ingredients for this recipe are in the inventory
        foreach (RecipeSystem.requiredIngredient req in recipeChecking.requiredIngredients)
            if (Inventory.CheckItem(req.ingredient.ToString()) < req.amount)//if there is not enough for any
                return false;
        return true; //if none of the ingredients were out of sotck
    }
    public static void Consume(string consumeItem, int amount)
    {//to remove an amount of an item from the list
        int index = inventoryCounts.FindIndex(ic => ic.item == consumeItem);//find this item already in list
        if (index != -1)
        {//if the item exists in the inventory, remove that amount
            inventoryCount myItem;
            myItem.item = consumeItem;
            myItem.amount = inventoryCounts[index].amount - amount;//find new inventory count
            if (myItem.amount > 0)//if there is still any left
                inventoryCounts[index] = myItem;//set the new amount
            else//if the item is out
                inventoryCounts.RemoveAt(index);
        }
        else //error when removing an item that isnt there
            Debug.Log("Removed item " + consumeItem + " which isnt in inventory. Possible string value typo. Make sure to check inventory with Inventory.CheckItem before removing");
    }
    public static void Consume(string consumeItem)
    {//overload assumes only 1 ofthe item is being consumed
        Consume(consumeItem, 1);
    }
    public static void Consume(List<RecipeSystem.requiredIngredient> removeList)
    {//overload specifically for recipes, removes a list of ingredient-amount pairs
        foreach (RecipeSystem.requiredIngredient req in removeList)
            Consume(req.ingredient.ToString(), req.amount);
    }
}

