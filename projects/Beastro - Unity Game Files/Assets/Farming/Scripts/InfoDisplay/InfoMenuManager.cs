using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoMenuManager : MonoBehaviour
{
    public PlantTemplate[] plantData;

    public GameObject plantImage;
    public GameObject plantName;
    public GameObject plantInfo;
    public void PotatoData()
    {
        plantImage.GetComponent<Image>().sprite = plantData[0].plantImage;
        plantName.GetComponent<Text>().text = plantData[0].plantName;
        int offset = plantData[0].totalTimeNeededToGrow / 2 - Mathf.RoundToInt((plantData[0].totalTimeNeededToGrow / 2) / 2);
        int perfect = plantData[0].totalTimeNeededToGrow / 2 + Mathf.RoundToInt((plantData[0].totalTimeNeededToGrow / 2) / 2);
        if (offset == 0) { offset = 1; }
        if (offset == 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[0].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[0].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> day; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[0].timeTillDeath + "</color> days after planting if not harvested.";
        if (offset > 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[0].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[0].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> days; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[0].timeTillDeath + "</color> days after planting if not harvested.";
    }

    public void CarrotData()
    {
        plantImage.GetComponent<Image>().sprite = plantData[1].plantImage;
        plantName.GetComponent<Text>().text = plantData[1].plantName;
        int offset = plantData[1].totalTimeNeededToGrow / 2 - Mathf.RoundToInt((plantData[1].totalTimeNeededToGrow / 2) / 2);
        int perfect = plantData[1].totalTimeNeededToGrow / 2 + Mathf.RoundToInt((plantData[1].totalTimeNeededToGrow / 2) / 2);
        if (offset == 0) { offset = 1; }
        if (offset == 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[1].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[1].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> day; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[1].timeTillDeath + "</color> days after planting if not harvested.";
        if (offset > 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[1].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[1].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> days; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[1].timeTillDeath + "</color> days after planting if not harvested.";
    }

    public void OnionData()
    {
        plantImage.GetComponent<Image>().sprite = plantData[2].plantImage;
        plantName.GetComponent<Text>().text = plantData[2].plantName;
        int offset = plantData[2].totalTimeNeededToGrow / 2 - Mathf.RoundToInt((plantData[2].totalTimeNeededToGrow / 2) / 2);
        int perfect = plantData[2].totalTimeNeededToGrow / 2 + Mathf.RoundToInt((plantData[2].totalTimeNeededToGrow / 2) / 2);
        if (offset == 0) { offset = 1; }
        if (offset == 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[2].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[2].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> day; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[2].timeTillDeath + "</color> days after planting if not harvested.";
        if (offset > 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[2].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[2].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> days; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[2].timeTillDeath + "</color> days after planting if not harvested.";
    }

    public void CabbageData()
    {
        plantImage.GetComponent<Image>().sprite = plantData[3].plantImage;
        plantName.GetComponent<Text>().text = plantData[3].plantName;
        int offset = plantData[3].totalTimeNeededToGrow / 2 - Mathf.RoundToInt((plantData[3].totalTimeNeededToGrow / 2) / 2);
        int perfect = plantData[3].totalTimeNeededToGrow / 2 + Mathf.RoundToInt((plantData[3].totalTimeNeededToGrow / 2) / 2);
        if (offset == 0) { offset = 1; }
        if (offset == 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[3].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[3].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> day; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[3].timeTillDeath + "</color> days after planting if not harvested.";
        if (offset > 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[3].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[3].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> days; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[3].timeTillDeath + "</color> days after planting if not harvested.";
    }

    public void WheatData()
    {
        plantImage.GetComponent<Image>().sprite = plantData[4].plantImage;
        plantName.GetComponent<Text>().text = plantData[4].plantName;
        int offset = plantData[4].totalTimeNeededToGrow / 2 - Mathf.RoundToInt((plantData[4].totalTimeNeededToGrow / 2) / 2);
        int perfect = plantData[4].totalTimeNeededToGrow / 2 + Mathf.RoundToInt((plantData[4].totalTimeNeededToGrow / 2) / 2);
        if (offset == 0) { offset = 1; }
        if (offset == 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[4].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[4].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> day; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[4].timeTillDeath + "</color> days after planting if not harvested.";
        if (offset > 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[4].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[4].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> days; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[4].timeTillDeath + "</color> days after planting if not harvested.";
    }

    public void PumpkinData()
    {
        plantImage.GetComponent<Image>().sprite = plantData[5].plantImage;
        plantName.GetComponent<Text>().text = plantData[5].plantName;
        int offset = plantData[5].totalTimeNeededToGrow / 2 - Mathf.RoundToInt((plantData[5].totalTimeNeededToGrow / 2) / 2);
        int perfect = plantData[5].totalTimeNeededToGrow / 2 + Mathf.RoundToInt((plantData[5].totalTimeNeededToGrow / 2) / 2);
        if (offset == 0) { offset = 1; }
        if (offset == 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[5].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[5].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> day; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[5].timeTillDeath + "</color> days after planting if not harvested.";
        if (offset > 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[5].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[5].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> days; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[5].timeTillDeath + "</color> days after planting if not harvested.";
    }

    public void TomatoData()
    {
        plantImage.GetComponent<Image>().sprite = plantData[6].plantImage;
        plantName.GetComponent<Text>().text = plantData[6].plantName;
        int offset = plantData[6].totalTimeNeededToGrow / 2 - Mathf.RoundToInt((plantData[6].totalTimeNeededToGrow / 2) / 2);
        int perfect = plantData[6].totalTimeNeededToGrow / 2 + Mathf.RoundToInt((plantData[6].totalTimeNeededToGrow / 2) / 2);
        if (offset == 0) { offset = 1; }
        if (offset == 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[6].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[6].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> day; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[6].timeTillDeath + "</color> days after planting if not harvested.";
        if (offset > 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[6].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[6].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> days; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[6].timeTillDeath + "</color> days after planting if not harvested.";
    }

    public void CornData()
    {
        plantImage.GetComponent<Image>().sprite = plantData[7].plantImage;
        plantName.GetComponent<Text>().text = plantData[7].plantName;
        int offset = plantData[7].totalTimeNeededToGrow / 2 - Mathf.RoundToInt((plantData[7].totalTimeNeededToGrow / 2) / 2);
        int perfect = plantData[7].totalTimeNeededToGrow / 2 + Mathf.RoundToInt((plantData[7].totalTimeNeededToGrow / 2) / 2);
        if (offset == 0) { offset = 1; }
        if (offset == 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[7].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[7].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> day; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[7].timeTillDeath + "</color> days after planting if not harvested.";
        if (offset > 1)
            plantInfo.GetComponent<Text>().text = "Days Needed To Grow: " + plantData[7].totalTimeNeededToGrow + "\nFertilizer Cost: " + plantData[7].fertilizerCost + "\nThe plant requires watering for at least <color=green>" + offset + "</color> days; <color=green>" + perfect + "</color> days for highest harvest.\nCrop will die either if not watered or <color=red>" + plantData[7].timeTillDeath + "</color> days after planting if not harvested.";
    }
}
