using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Recipe{//holds information and references for a recipe
    public string name;
    public string description;
    public Sprite image;
    public GameObject spawnable;
    public List<RecipeSystem.requiredIngredient> requiredIngredients;//list of ingredients with amount needed
    public RecipeSystem.station station;
    public int price;
    public float cookDuration = 3;
}

[CreateAssetMenu(fileName = "RecipeSystem", menuName = "Restaurant/RecipeSystem")]
public class RecipeSystem : ScriptableObject
{
    public enum ingredients{beef, chicken, bread, salt, pepper};//possible ingredients, will need extending. might be moved somewhere else
    public enum station{stove, blender, coffeePot, frier, cuttingboard, microwave}//list of cooking stations that needs added to as more stations are made
    [Serializable]
    public struct requiredIngredient{//structure for linking ingredient to the amount needed in the recipe
        public ingredients ingredient;
        public int amount;
    }
    public List<Recipe> recipes;//recipe defintions on the asset
    public static bool IsIngredient(string name){//cannot loop through an enum so this method needs to be UPDATED every time an INGREDIENT IS ADDED
        switch(name){
            case "beef":
                return true;
            case "chicken":
                return true;
            case "bread":
                return true;
            case "salt":
                return true;
            case "pepper":
                return true;
            default:
                return false;
        }
    }
    public static float PercentSimilar(Recipe compare, Recipe original){//gives percent similar based on how many ingredients are in common. same recipe is 100%, no ingredients in common is 0%
        float matches = 0, total = 0;
        foreach(requiredIngredient req in original.requiredIngredients){
            if (compare.requiredIngredients.Exists(x => x.ingredient == req.ingredient))//check for ingredient directly, avoiding any issues like one needing a different amount of the same item
                matches++;
            total++;
        }
        return matches / total;//output percent between 0 and 1
    }
}