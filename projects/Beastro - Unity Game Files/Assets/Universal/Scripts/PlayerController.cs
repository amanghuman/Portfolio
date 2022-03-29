using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public CharacterController controller;

    private Vector3 moveDirection;
    public float gravityScale;

    public Animator anim;
    public Transform pivot;
    public float rotateSpeed;

    public GameObject playerModel;

    public bool jumped = false;

    //only used by attack script for hunting, should remain false for other scenes
    //used to restrict jumping/movement when attacking
    public bool throwing = false;
    public bool slashing = false;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Movement of character
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
        moveDirection = moveDirection.normalized * moveSpeed;
        moveDirection.y = yStore;

        // Player jump
        if (controller.isGrounded && !slashing && !throwing && jumped == false)
        {
            moveDirection.y = 0f;
            if (Input.GetButtonDown("Jump"))
            {
                jumped = true;
                moveDirection.y = jumpForce;
                StartCoroutine(JumpWait());
            }
        }


        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale);
        if (!throwing && !slashing)
            controller.Move(moveDirection * Time.deltaTime);

        // Move player in different directions based on camera direction
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        anim.SetBool("Jumped", jumped);
        anim.SetBool("Throwing", throwing);
        anim.SetBool("Slashing", slashing);
        anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }

    IEnumerator JumpWait()
    {
        yield return new WaitForSeconds(1.2f);
        jumped = false;
    }
}
