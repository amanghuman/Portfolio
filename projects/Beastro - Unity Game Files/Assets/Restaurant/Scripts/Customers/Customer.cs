using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : Interactive
{
    RestaurantPlayerController player;
    Animator anim;
    CustomerOrderBubble bubble;
    public BoxCollider InteractiveBox;
    public GameObject receivedFood;
    public bool served;
    public RecipeSystem recipeList;
    Recipe myOrder;
    public bool seated = false;
    //Seat seat;
    float timeSeated = 0, timeToWait = 90f;
    CustomerAI AI;
    CustomerEmote emoter;
    public Sprite[] reactions;
    public bool leaving;

    void Start(){
        //setup references
        player = GameObject.FindWithTag("Player").GetComponent<RestaurantPlayerController>();
        anim = GameObject.Find("Player/Customizable Player").GetComponent<Animator>();
        bubble = GetComponentsInChildren<CustomerOrderBubble>()[0];
        AI = GetComponent<CustomerAI>();
        emoter = GetComponentInChildren<CustomerEmote>();
        /*find a seat and go to it
        seat = Seat.TakeOpenSeat();
        if (seat == null) 
            Destroy(gameObject);//despawn if there are not any seats
        else
            TakeSeat();//will later be called when they get to their seat
        */
    }
    void Update(){
        if (seated && !served){//update waiting timer
            float perc = 1f - (Time.time - timeSeated) / timeToWait;
            bubble.UpdateTimer(perc);
            if (Time.time > timeSeated + timeToWait){
                LeaveSeat();
            }
        }
    }
    protected override void OnInteract(){
        // Makes Unity Chan give the food to the customer
        if (player.holdingPlate && !served && player.holdingPlate)
        {
            //check correctness of order
            float percCorrect = RecipeSystem.PercentSimilar(myOrder, player.holdingRecipe);
            float percTiming = 1f - (Time.time - timeSeated) / timeToWait;
            int tip = CalulateTip(percCorrect, percTiming, myOrder.price);
            emoter.Emote(PickEmote(percCorrect));
            //get player out of holding state
            player.holdingPlate = false;
            anim.SetBool("Holding", false);
            //move the food in front of the customer and exchange money
            GameObject child = player.holdingPosition.GetChild(0).gameObject;
            child.transform.position = receivedFood.transform.position;
            child.transform.parent = receivedFood.transform;
            Money.AddMoney(myOrder.price + tip);//if there are multiple items in an order this will need to be updated
            //get customer out of order receiving
            bubble.gameObject.SetActive(false);
            InteractiveBox.enabled = false;//disable interactive
            ManualRemove();//removes from interactive list
            served = true;
        }
    }
    public void TakeSeat(){
        //align with seat position
        transform.position = AI.seat.transform.position + AI.seat.seatedOffset;
        transform.rotation = Quaternion.LookRotation(AI.seat.facingDirection, Vector3.up);
        //give order bubble its image and turn it on
        PickOrder();
        InteractiveBox.enabled = true;
        bubble.Setup(myOrder.image);

        seated = true;
        timeSeated = Time.time;
    }
    void PickOrder(){//pick an order from the recipeList, currently picks one at random
        int index = Mathf.FloorToInt(Random.value * (recipeList.recipes.Count - 0.1f));//get a random valid index
        myOrder = recipeList.recipes[index];//hold the reference for use later
    }
    public void LeaveSeat(){
        seated = false;
        AI.seat.Taken = false;
        AI.seat = null;
        //get customer out of order receiving
        bubble.gameObject.SetActive(false);
        InteractiveBox.enabled = false;//disable interactive
        ManualRemove();//removes from interactive list
        leaving = true;
        //gameObject.SetActive(false);//for now just deactiving customer since walking isnt in
    }
    int CalulateTip(float correctness, float timing, float price){//give tip
        float baseTip = 0.2f;//basing tip on a percentage of item price
        float tipPercCorrectness = baseTip * correctness;//20% tip on 100% correct order
        float tipPercTiming = baseTip * timing;//up to 20% tip for speed
        return (int) ((tipPercCorrectness + tipPercTiming) * price);//return the amount to add as a tip (at most 40%)
    }
    Sprite PickEmote(float correctness){///returns a emoji from a sprite list based on how correct the order was
        //print(correctness);
        if (correctness > 0.8f)
            return reactions[0];
        if (correctness > 0.6f)
            return reactions[1];
        if (correctness > 0.4f)
            return reactions[2];
        return reactions[3];
    }
}
