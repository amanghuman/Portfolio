using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AirFryerButtonManager : MonoBehaviour
{
    PauseGameManager paused;

    public GameObject foodMenu;

    GameObject foodSpawn;
    public RecipeSystem recipeList;
    public Image selectedImage;
    public Text selectedName, selectedDesc, selectedIngredients;

    AirFryer selectedAirFryer;

    public GameObject[] cookedFoodPrefab;
    /*   0: Burger
     *   1: Chicken
     */

    void Start()
    {
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
    }


    public void AirFryerCook(AirFryer airFryer)
    {
        foodMenu.SetActive(true);
        paused.PauseGame();
        foodSpawn = airFryer.foodSpawn;
        selectedAirFryer = airFryer;
    }

    public void CloseMenu()
    {
        // Unpauses the game and disables the recipe menu.
        foodMenu.SetActive(false);
        paused.UnPauseGame();
    }

    public void CookFood(Recipe recipe){
        if (Inventory.CheckRecipe(recipe)){//if the ingredients are there
            Inventory.Consume(recipe.requiredIngredients);//consume the ingredients
            Instantiate(recipe.spawnable, foodSpawn.transform.position, Quaternion.identity, foodSpawn.transform);// Spawns a burger to the location it is called on.
            foodMenu.SetActive(false);//close menu
            paused.UnPauseGame();
            selectedAirFryer.StartCooking();
        }
        else{//required ingredients not met
            print("Required Ingredients not met for " + recipe.name);
        }
    }
    public void CookFrenchFries()
    {
        Recipe Fries = recipeList.recipes.Find(x => x.name == "French Fries");//find recipe for this item
        CookFood(Fries);//pass recipe to generic logic
    }

    public void CookTempura()
    {
        Recipe Tempura = recipeList.recipes.Find(x => x.name == "Tempura");//find recipe for this item
        CookFood(Tempura);//pass recipe to generic logic
    }

    public void CookShrimp()
    {
        Recipe Shrimp = recipeList.recipes.Find(x => x.name == "Shrimp");//find recipe for this item
        CookFood(Shrimp);//pass recipe to generic logic
    }

    public void SelectFries(){
        SetSelected(recipeList.recipes.Find(x => x.name == "French Fries"));
    }

    public void SelectTempura(){
        SetSelected(recipeList.recipes.Find(x => x.name == "Tempura"));
    }

    public void SelectShrimp()
    {
        SetSelected(recipeList.recipes.Find(x => x.name == "Shrimp"));
    }

    public void SetSelected(Recipe selectedRecipe){//used to set the preview screen of the item currently hovered
        //selectedName.text = selectedRecipe.name;//set each field to represent that recipe
        selectedImage.sprite = selectedRecipe.image;
        selectedDesc.text = selectedRecipe.name;
        selectedIngredients.text = "";//reset ingredients text
        foreach(RecipeSystem.requiredIngredient elem in selectedRecipe.requiredIngredients){//print the ingredients to a text feild
            selectedIngredients.text += elem.ingredient.ToString() + " required: " + elem.amount.ToString() + 
                                        ", inventory: " + Inventory.CheckItem(elem.ingredient.ToString()) + " \n";//add ingredients in loop
        }
        selectedIngredients.text = selectedIngredients.text.Substring(0, selectedIngredients.text.Length - 2);//remove the last comma
    }
}
