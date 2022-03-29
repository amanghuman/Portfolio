using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RestaurantPlayerController : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController controller;
    PauseGameManager paused;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;

    public GameObject pauseMenu;

    public bool holdingPlate = false;
    [HideInInspector]
    public Recipe holdingRecipe;
    public Transform holdingPosition;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        paused = GameObject.Find("GamePauseManager").GetComponent<PauseGameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        // Movement of character
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        controller.Move(moveDirection * Time.deltaTime);

        // Move player in different directions based on camera direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        // Set animation when holding plate
        if (holdingPlate)
        {
            anim.SetBool("Holding", true);
        }
        else
        {
            anim.SetBool("Holding", false);
        }

        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));

        //interaction checking
        if (Input.GetButtonDown("Interact"))
        {
            Interactive.Interact();//interactable system will call to the nearest item that you can interact with
            anim.SetFloat("Speed", 0.0f); // Stop player from an animation loop when pausing and 
                                          // interacting with things at the same time.
        }

        // Basic open menu button
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            // Can add animation down the road
            if (!paused.pause) // Pauses the game
            {
                pauseMenu.SetActive(true);
                paused.PauseGame();
            }
            else // Unpauses the game
            {
                pauseMenu.SetActive(false);
                paused.UnPauseGame();
            }
        }
    }

    public void PickUpPlate(Recipe recipe, GameObject foodObject){//player recieves food from station and puts it in her hands
        //set as holding plate
        holdingRecipe = recipe;
        holdingPlate = true;
        anim.SetBool("Holding", true);
        //move plate to players hands
        foodObject.transform.position = holdingPosition.position;
        foodObject.transform.parent = holdingPosition;
    }
}
