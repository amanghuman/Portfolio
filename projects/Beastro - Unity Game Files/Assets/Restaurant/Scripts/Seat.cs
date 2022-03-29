using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles data for a seat in the game, allows general access to an empty seat
public class Seat : MonoBehaviour
{
    static List<Seat> available = new List<Seat>();
    bool taken = false;
    public bool Taken{ //add or remove from available as taken changes
        set {
            if (value)//if seat will be taken
                available.Remove(this);
            else if (taken)//if seat is becoming available
                available.Add(this);
            taken = value;
        } 
        get {
            return taken;
        }
    }
    public Vector3 facingDirection;
    public Vector3 seatedOffset;

    void Start()
    {
        if (!taken)
            available.Add(this);//set upp the available list as each spawns
    }

    public static Seat TakeOpenSeat(){// returns an open seat, and sets that seat to taken
        if (available.Count == 0)
            return null;
        //gets a random open seat, this could be modified to get the cloest seat to the front or some other desireable seat
        int index = Mathf.FloorToInt(Random.Range(0, available.Count - 0.01f));
        Seat ret = available[index];//caputre a reference to the seat
        ret.Taken = true;//remove it from the available list 
        return ret;
    }
}
