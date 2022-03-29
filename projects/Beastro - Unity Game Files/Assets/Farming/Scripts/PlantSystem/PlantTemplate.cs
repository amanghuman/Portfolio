using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Plant", menuName = "Plant")]
public class PlantTemplate : ScriptableObject {

        //Plant unique Identifier ID
        public int uniqueID;
        
        public string plantName;
        //public string plantType;
        //public string plantDescription;

        public Sprite plantImage;
        //public int secondsInMinute = 60;
        public int fertilizerCost;

        //Number of buckets needed to grow the plant
        //public int waterCost;

        //Time needed to grow
        public int totalTimeNeededToGrow;

        //Time remaining till fully grown
        public int timeRemainingToGrow;

        //Time since plant is last watered
        public int timeSinceWatered;

        //Time when crop dies
        public int timeTillDeath;
        //Season the plant can grow
        //public string season;
}
