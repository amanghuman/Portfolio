using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactive : MonoBehaviour
{
    static List<(Interactive i, float dist)> interactions = new List<(Interactive i, float dist)>(); //list of all things the player could interact with, as tuple with distance
    
    protected virtual void OnInteract(){}
    public Transform PopupLocation;//reference to gameobject location for the "press to interact" popup
    Vector3 popupDefaultLocation = new Vector3(0f,1f,0f);//default popup location, incase one hasnt been set

    public static void Interact(){//picks the closest interactive and calls its interact
        Interactive inter = PeekInteract();//find the closests intertactive to the player
        if (inter != null)
            inter.OnInteract();//only call intereact on the closest one
    }

    public static (Vector3 pos, bool valid) UIPosition(){//Returns position for Ineraction indicator. Prefab "InteractCanvas" is required to be in the scene for this to be called. 
        //get reference and null check
        Interactive inter = PeekInteract();
        if (inter == null)//if theres nothing to interact with
            return (Vector3.zero, false);//Vector3 is non-nullable. This is more clear than picking a random null value
        
        //find the correct position
        Vector3 ret;
        ret = inter.transform.position;//get position of the interactive 
        //add the offset so its in the correct position
        ret += (inter.PopupLocation == null) ? inter.popupDefaultLocation : inter.PopupLocation.localPosition;
        
        return (ret, true);
    }

    static Interactive PeekInteract(){//show which object would be interacted with right now
        //keep track and iterate
        float min = 50;//distance cutoff
        int index = -1;//stores closests index so far
        for (int i = 0; i < interactions.Count; i++){//serach all the interactions
            if (interactions[i].dist < min){//keep track of the closest one
                min = interactions[i].dist;
                index = i;
            }
        }
        if (index != -1)//null check
            return interactions[index].i;//return closest interactive
        return null;//return null if none were found
    }

    void OnTriggerEnter(Collider other)//keep track of an interactive while the player is inside its trigger
    {
        if (other.CompareTag("Player")){
            float dist = Vector3.Distance(transform.position, other.transform.position);
            interactions.Add((this,dist));//add to interactive list
        }
    }

    void OnTriggerStay(Collider other)//update the distance while inside
    {
        if (other.CompareTag("Player")){
            float dist = Vector3.Distance(transform.position, other.transform.position);
            for (int i = 0; i < interactions.Count; i++){//search for this interactive
                if (interactions[i].i == this){
                    interactions[i] = (this, dist);//update the distance
                }
            }
        }
    }

    void OnTriggerExit(Collider other)//remove from interactions on exit
    {
        if (other.CompareTag("Player")){
            // print("Player Exit");
            interactions.RemoveAll(IsInteractiveThisOne);//remove all for safety, uses predicate bellow
        }
    }
    protected void ManualRemove(){//for cases where the player is in the trigger when it is dissabled
        interactions.RemoveAll(IsInteractiveThisOne);
    }
    private bool IsInteractiveThisOne ((Interactive i, float dist) elem){//predicate function for remove all
        return elem.i == this;
    }

}
