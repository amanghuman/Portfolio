using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomization : MonoBehaviour
{
    PauseGameManager paused;

    public GameObject playerCustomMenu;

    public GameObject customizationModel;

    // For the controllable player model.
    public GameObject[] playerAccessory;
    public GameObject[] playerCostume;
    public GameObject[] playerSkin;
    public GameObject[] playerEyes;
    public GameObject[] playerHairBase;
    public GameObject[] playerHairFront;

    // For the customization menu model.
    public GameObject[] customAccessory;
    public GameObject[] customCostume;
    public GameObject[] customSkin;
    public GameObject[] customEyes;
    public GameObject[] customHairBase;
    public GameObject[] customHairFront;

    // For customization values
    int accessory = 3;
    int costume = 2;
    int eyes = 1;
    int hair = 0;

    public float rotateY;
    bool isRightDown;
    bool isLeftDown;

    // Start is called before the first frame update
    void Start()
    {
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
        isRightDown = false;
        isLeftDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRightDown)
            // Rotate customization model to the right
            customizationModel.transform.Rotate(0.0f, -rotateY, 0.0f, Space.Self);

        if (isLeftDown)
            // Rotate customization model to the left
            customizationModel.transform.Rotate(0.0f, rotateY, 0.0f, Space.Self);
    }

    // Those methods are called from the EventTrigger component of the button right and button Left
    // use Pointer Up and Pointer Down and match them accordingly
    public void RightDown() { isRightDown = true; }
    public void RightUp() { isRightDown = false; }
    public void LeftUp() { isLeftDown = false; }
    public void LeftDown() { isLeftDown = true; }

    // All of these handle changing the outfit choice depending on the direction pressed.
    public void LeftAccessory()
    {
        if (accessory != 3)
        {
            playerAccessory[accessory].SetActive(false);
            customAccessory[accessory].SetActive(false);
        }
        accessory++;
        if (accessory > customAccessory.Length)
            accessory = 0;
        if (accessory == 3)
            return;
        Debug.Log(accessory);
        playerAccessory[accessory].SetActive(true);
        customAccessory[accessory].SetActive(true);
        
    }

    public void RightAccessory()
    {
        if (accessory != 3)
        {
            playerAccessory[accessory].SetActive(false);
            customAccessory[accessory].SetActive(false);
        }
        accessory--;
        if (accessory < 0)
            accessory = customAccessory.Length;
        if (accessory == 3)
            return;
        playerAccessory[accessory].SetActive(true);
        customAccessory[accessory].SetActive(true);
    }

    public void LeftCustome()
    {
        playerCostume[costume].SetActive(false);
        playerSkin[costume].SetActive(false);
        customCostume[costume].SetActive(false);
        customSkin[costume].SetActive(false);
        costume++;
        if (costume > customCostume.Length - 1)
            costume = 0;
        playerCostume[costume].SetActive(true);
        playerSkin[costume].SetActive(true);
        customCostume[costume].SetActive(true);
        customSkin[costume].SetActive(true);
    }

    public void RightCustome()
    {
        playerCostume[costume].SetActive(false);
        playerSkin[costume].SetActive(false);
        customCostume[costume].SetActive(false);
        customSkin[costume].SetActive(false);
        costume--;
        if (costume < 0)
            costume = customCostume.Length - 1;
        playerCostume[costume].SetActive(true);
        playerSkin[costume].SetActive(true);
        customCostume[costume].SetActive(true);
        customSkin[costume].SetActive(true);
    }

    public void LeftHair()
    {
        playerHairBase[hair].SetActive(false);
        playerHairFront[hair].SetActive(false);
        customHairBase[hair].SetActive(false);
        customHairFront[hair].SetActive(false);
        hair++;
        if (hair > customHairBase.Length - 1)
            hair = 0;
        playerHairBase[hair].SetActive(true);
        playerHairFront[hair].SetActive(true);
        customHairBase[hair].SetActive(true);
        customHairFront[hair].SetActive(true);
    }

    public void RightHair()
    {
        playerHairBase[hair].SetActive(false);
        playerHairFront[hair].SetActive(false);
        customHairBase[hair].SetActive(false);
        customHairFront[hair].SetActive(false);
        hair--;
        if (hair < 0)
            hair = customHairBase.Length - 1;
        playerHairBase[hair].SetActive(true);
        playerHairFront[hair].SetActive(true);
        customHairBase[hair].SetActive(true);
        customHairFront[hair].SetActive(true);
    }

    public void LeftEyes()
    {
        playerEyes[eyes].SetActive(false);
        customEyes[eyes].SetActive(false);
        eyes++;
        if (eyes > customEyes.Length - 1)
            eyes = 0;
        playerEyes[eyes].SetActive(true);
        customEyes[eyes].SetActive(true);
    }

    public void RightEyes()
    {
        playerEyes[eyes].SetActive(false);
        customEyes[eyes].SetActive(false);
        eyes--;
        if (eyes < 0)
            eyes = customEyes.Length - 1;
        playerEyes[eyes].SetActive(true);
        customEyes[eyes].SetActive(true);
    }

    public void ConfrimButton()
    {
        // Turns the player customization window off and unpauses the game.
        playerCustomMenu.SetActive(false);
        customizationModel.transform.rotation = Quaternion.Euler(0,180,0);
        paused.UnPauseGame();
    }
}
