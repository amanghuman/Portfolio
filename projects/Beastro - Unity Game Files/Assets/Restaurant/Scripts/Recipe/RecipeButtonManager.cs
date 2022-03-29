using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RecipeButtonManager : MonoBehaviour
{
    PauseGameManager paused;

    public GameObject foodMenu;
    public RecipeSystem recipeList;
    public RectTransform menuList;
    public GameObject recipeButtonPrefab;
    public GameObject infoMenuRoot;
    public Image selectedImage;
    public Text selectedName, selectedDesc, selectedIngredients;
    // RecipeSystem.station stationType;
    Station currentStation;

    void Start()
    {
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
    }
    public void StationCook(Station station){
        foodMenu.SetActive(true);
        paused.PauseGame();
        currentStation = station;
        // stationType = station.stationType;
        UpdateMenu(station.stationType);
    }

    // public void StoveCook(Stove stove)
    // {
    //     foodMenu.SetActive(true);
    //     paused.PauseGame();
    //     currentStation = stove;
    //     stationType = RecipeSystem.station.stove;
    //     UpdateMenu(stationType);
    // }

    // public void EspressoCook(Espresso espresso){
    //     foodMenu.SetActive(true);
    //     paused.PauseGame();
    //     currentStation = espresso;
    //     stationType = RecipeSystem.station.coffeePot;
    //     UpdateMenu(stationType);
    // }
    // public void AirFryerCook(AirFryer airFryer){
    //     foodMenu.SetActive(true);
    //     paused.PauseGame();
    //     currentStation = airFryer;
    //     stationType = RecipeSystem.station.fryer;
    //     UpdateMenu(stationType);
    // }

    // public void BlenderCook(Blender blender)
    // {
    //     foodMenu.SetActive(true);
    //     paused.PauseGame();
    //     currentStation = blender;
    //     stationType = RecipeSystem.station.blender;
    //     UpdateMenu(stationType);
    // }
    // public void CutCook(CuttingBoard cutting)
    // {
    //     foodMenu.SetActive(true);
    //     paused.PauseGame();
    //     currentStation = cutting;
    //     stationType = RecipeSystem.station.cuttingBoard;
    //     UpdateMenu(stationType);
    // }
    void UpdateMenu(RecipeSystem.station station){
        //clear the menu
        for (int i = 0; i < menuList.childCount; i++)
            Destroy(menuList.GetChild(i).gameObject);
        //insert the menu buttons for this station
        for (int i = 0; i < recipeList.recipes.Count; i++){//loop through all recipes
            if (recipeList.recipes[i].station == station){//check if this element needs added for this station
                //create and set up the button
                Recipe recipe = recipeList.recipes[i];
                GameObject buttonObj = Instantiate(recipeButtonPrefab, menuList.transform.position, Quaternion.identity);
                buttonObj.transform.parent = menuList.transform;
                buttonObj.transform.localScale = Vector3.one;
                RectTransform rect = buttonObj.GetComponent<RectTransform>();
                rect.sizeDelta = new Vector2(rect.sizeDelta.x, 80f);//set to a specific height (unity doesnt like to let the prefab do this, set to 0 or 10 no matter the setup of the scroll rect)
                buttonObj.GetComponentInChildren<Text>().text = recipe.name;
                //add button callbacks
                buttonObj.GetComponent<Button>().onClick.AddListener(delegate {CookFood(recipe);});//callback for button press with recipe param
                EventTrigger trigger = buttonObj.GetComponent<EventTrigger>();
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((data) => {SetSelected(recipe);});//callback for hovering
                trigger.triggers.Add(entry);
            }
        }
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
            foodMenu.SetActive(false);//close menu
            paused.UnPauseGame();
            currentStation.StartCooking(recipe);
            infoMenuRoot.SetActive(false);
            // if (stationType == RecipeSystem.station.stove)
            // {
            //     Stove stove = (Stove)currentStation;
            //     stove.StartCooking(recipe);
            // }
            // else if (stationType == RecipeSystem.station.coffeePot)
            // {
            //     Espresso espresso = (Espresso)currentStation;
            //     espresso.StartCooking(recipe);
            // }
            // else if (stationType == RecipeSystem.station.fryer)
            // {
            //     AirFryer fryer = (AirFryer)currentStation;
            //     fryer.StartCooking(recipe);
            // }
            // else if (stationType == RecipeSystem.station.blender)
            // {
            //     Blender blender = (Blender)currentStation;
            //     blender.StartCooking(recipe);
            // } 
            // else if (stationType == RecipeSystem.station.cuttingBoard)
            // {
            //     CuttingBoard cutting = (CuttingBoard)currentStation;
            //     cutting.StartCooking(recipe);
            // }
        }
        else{//required ingredients not met
            print("Required Ingredients not met for " + recipe.name);
        }
    }
    public void SetSelected(Recipe selectedRecipe){//used to set the preview screen of the item currently hovered
        //selectedName.text = selectedRecipe.name;//set each field to represent that recipe
        infoMenuRoot.SetActive(true);
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
