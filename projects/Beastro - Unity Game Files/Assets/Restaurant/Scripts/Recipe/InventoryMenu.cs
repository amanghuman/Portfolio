using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    public Transform ingredientContent;
    public GameObject ingredientPrefab;
    void Awake(){
        ResetInventoryMenu();
    }

    void ResetInventoryMenu(){
        //clear out menu
        for (int i = 0; i < ingredientContent.transform.childCount; i++)
            Destroy(ingredientContent.transform.GetChild(i).gameObject);
        //fill ingredients
        for (int i = 0; i < Inventory.inventoryCounts.Count; i++){
            if (!RecipeSystem.IsIngredient(Inventory.inventoryCounts[i].item))
                continue;//skip non-ingredient items
            IngredientMenuItem item = Instantiate(ingredientPrefab, ingredientContent.position, Quaternion.identity).GetComponent<IngredientMenuItem>();
            item.transform.parent = ingredientContent;//make it part of the scroll rect
            item.ingredient.text = Inventory.inventoryCounts[i].item;//sert the items values on the UI
            item.number.text = Inventory.inventoryCounts[i].amount.ToString();
            item.transform.localScale = Vector3.one;//have to compensate for the canvas bing at an odd scale
        }
    }
}
