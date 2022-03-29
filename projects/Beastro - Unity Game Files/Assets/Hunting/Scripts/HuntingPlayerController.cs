using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingPlayerController : MonoBehaviour
{
    // Iteration 4 ea
    [SerializeField] private float slopeForce;
    [SerializeField] private float slopeForceRayLength;

    //private Vector3 _velocity;
    /////////////////////////////  

    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float gravityScale;
    public float cameraRotationSpeed; // Iteration 3 (rename)

    // Cooldown variables
    public float dodgeCooldownTime; // Iteration 3
    public float slashCooldownTime; // Iteration 3
    public float throwCooldownTime; // Iteration 3

    public float stunCooldownTime;  // Iteration 5 ea

    // Utility
    private Transform           pivot;
    private CharacterController controller;
    private GameObject          playerModel;
    private Animator            animator;
    private PlayerHealth        playerHealth;

    // Movement Variables
    private Vector3 moveDirection;
    private float   moveSpeedMultiplier;
    private float   horizontalInput;
    private float   verticalInput;

    // Animation Variables 
    private bool throwing;  // Iteration 3 (changed access)
    private bool slashing;  // Iteration 3 (changed access)
    private bool jumping;   // Iteration 3 (changed name, access)
    private bool dodging;   // Iteration 3
    public  bool takingDamage; // Iteration 3 ea
    public  bool stunning;  // Iteration 5 ea 

    // Cooldown for dodging
    private bool inDodgeCooldown; // Iteration 3 (rename)
    private bool inSlashCooldown; // Iteration 3
    private bool inThrowCooldown; // Iteration 3
    private bool inStunCooldown; // Iteration 5

    // For AnimationCooldown
    private enum cooldownSpecifier { Dodge, Jump, Throw, Slash, TakeDmg, Stun } // Iteration 3

    public GameObject abilityUI;
    private AbilityCooldown abilityCooldown;

    // Start is called before the first frame update
    private void Start()
    {
        // Iteration 3
        pivot = GameObject.Find("CameraBase").transform;
        controller = GetComponent<CharacterController>();
        playerModel = transform.Find("Customizable Player").gameObject;
        animator = playerModel.GetComponent<Animator>();
        playerHealth = GetComponent<PlayerHealth>(); // Iteration 3 ea

        jumping = false;
        dodging = false;
        throwing = false;
        slashing = false;
        takingDamage = false; // Iteration 3 ea
        stunning = false;     // Iteration 5 ea

        inDodgeCooldown = false; // Iteration 3 (rename)
        inSlashCooldown = false; // Iteration 3
        inThrowCooldown = false; // Iteration 3
        inStunCooldown = false;  // Iteration 5 ea
        abilityCooldown = abilityUI.GetComponent<AbilityCooldown>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (playerHealth.playerDead == false) // Iteration 3 ea
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Interact();

            if (canStun())
                StunAbility();  // Iteration 5 ea

            if (canDodge())
                Dodge();

            if (canJump())
                Jump();

            if (canSlash()) // Iteration 3
                Slash();

            if (canThrow()) // Iteration 3
                Throw();

            Move();

            // Iteration 3
            if (canTurn())
                Turn();

            //Debug.Log(slashing);

            UpdateAnimator();
        }
    }

    private void StunAbility() {
        if (Input.GetButtonDown("Ability"))
        {
            // animator.Play("Stun AOE");
            stunning = true;            
        }
    }

    private void Interact()
    {
        if (Input.GetButtonDown("Interact"))
            Interactive.Interact();    //interactable system will call to the nearest item that you can interact with
    }

    private void Dodge()
    {
        if (Input.GetAxis("Dodge") > 0.1)
        {
            dodging = true;

            // Ensure that the player's speed is correct
            moveSpeedMultiplier = runSpeed*0.75f;

            float yStore = moveDirection.y;
            moveDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
            moveDirection = moveDirection.normalized * moveSpeedMultiplier;
            moveDirection.y = yStore;
        }
    }

    private void Jump()
    {
        moveDirection.y = 0f;
        if (Input.GetAxis("Jump") > 0.1) //
        {
            jumping = true;
            moveDirection.y = jumpForce;
            StartCoroutine(animationCooldown(cooldownSpecifier.Jump)); // Iteration 3
        }
    }

    private void Move()
    {
        // Dodge Movement
        if (dodging)
        {
            moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
            controller.Move(moveDirection * Time.deltaTime);
            return;
        }

        moveSpeedMultiplier = walkSpeed;

        // Update movement speed if player is running
        if (Input.GetAxis("Run") > 0.1 && !dodging)
            moveSpeedMultiplier = runSpeed;

        // Get player input for direction
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * verticalInput) + (transform.right * horizontalInput);
        moveDirection = moveDirection.normalized * moveSpeedMultiplier;
        moveDirection.y = yStore;

        // Move player
        moveDirection.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
        if (!throwing && !slashing)
            controller.Move(moveDirection * Time.deltaTime);

        // Iteration 4 ea
        if ((verticalInput != 0 || horizontalInput != 0) && OnSlope())
        {
            controller.Move(Vector3.down * controller.height / 2 * slopeForce * Time.deltaTime);
        }
    }

    private void Turn() // Iteration 3 - rename
    {
        // Move player in different directions based on camera direction
        if (horizontalInput != 0 || verticalInput != 0)
        {
            transform.rotation = Quaternion.Euler(0f, pivot.rotation.eulerAngles.y, 0f);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0f, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, cameraRotationSpeed * Time.deltaTime);
        }
    }

    // Iteration 3
    private void Slash()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            slashing = true;
            WeaponInventory.enablePrimary(true);
            //WeaponInventory.usePrimary(); // Iteration 3 ea, anim event
        }
    }

    // Iteration 3
    private void Throw()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            throwing = true;
            // Using weapon occurs as animation event
        }
    }

    // Iteration 3 (updated)
    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", new Vector2(moveDirection.x, moveDirection.z).magnitude);
        animator.SetBool("Jumped", jumping);
        animator.SetBool("Dodged", dodging);
        animator.SetBool("Throwing", throwing);
        animator.SetBool("Slashing", slashing);
        animator.SetBool("Stunning", stunning);
        animator.SetBool("TakingDamage", takingDamage); // Iteration 3 ea
    }

    public bool isDodging() { return dodging; }

    public void endDodge()
    {
        dodging = false;
        inDodgeCooldown = true; // Iteration 3 (rename)
        StartCoroutine(animationCooldown(cooldownSpecifier.Dodge)); // Iteration 3
    }

    public void endStun()  // Iteration 5 ea
    {
        stunning = false;
        inStunCooldown = true;
        abilityCooldown.UseAbility();
        StartCoroutine(animationCooldown(cooldownSpecifier.Stun));
    }

    // Iteration 3
    public void endSlash()
    {
        slashing = false;
        inSlashCooldown = true;
        WeaponInventory.enablePrimary(false);
        StartCoroutine(animationCooldown(cooldownSpecifier.Slash));
    }

    // Iteration 3
    public void endThrow()
    {
        throwing = false;
        inThrowCooldown = true;
        StartCoroutine(animationCooldown(cooldownSpecifier.Throw));
    }

    // Iteration 3
    private IEnumerator animationCooldown(cooldownSpecifier i)
    {
        switch (i)
        {
            case cooldownSpecifier.Dodge:
                yield return new WaitForSeconds(dodgeCooldownTime);
                inDodgeCooldown = false;
                break;
            case cooldownSpecifier.Jump:
                yield return new WaitForSeconds(0.6f);
                jumping = false;
                break;
            case cooldownSpecifier.Slash:
                yield return new WaitForSeconds(slashCooldownTime);
                inSlashCooldown = false;
                break;
            case cooldownSpecifier.Throw:
                yield return new WaitForSeconds(throwCooldownTime);
                inThrowCooldown = false;
                break;
            case cooldownSpecifier.Stun:
                yield return new WaitForSeconds(8);
                inStunCooldown = false;
                break;
        }
    }

    private bool canStun()      // Iteration 5 ea
    {
        return inStunCooldown == false
                && !dodging
                && !slashing
                && !throwing
                && !stunning;
    }

    private bool canDodge()
    {
        return inDodgeCooldown == false // Iteration 3 (rename)
                && !dodging
                && !jumping // iteration 3 (changed syntax)
                && !slashing
                && !throwing
                && (moveDirection.x != 0 || moveDirection.z != 0)
                && !takingDamage // Iteration 3 ea
                && !stunning;    // Iteration 5 ea
    }

    private bool canJump()
    {
        return controller.isGrounded
                && !slashing
                && !throwing
                && !jumping
                && !dodging
                && !takingDamage // Iteration 3 ea
                && !stunning;    // Iteration 5 ea
    }

    // Iteration 3
    private bool canSlash()
    {
        return !jumping
                && !throwing
                && !slashing
                && !dodging
                && !inSlashCooldown
                && !takingDamage             // Iteration 3 ea
                && controller.isGrounded
                && !stunning;               // Iteration 5 ea
    }

    // Iteration 3
    private bool canThrow()
    {
        return !jumping
                && !throwing
                && !slashing
                && !dodging
                && !inThrowCooldown
                && !takingDamage             // Iteration 3 ea
                && controller.isGrounded
                && !stunning;                // Iteration 5 ea
    }

    // Iteration 3
    private bool canTurn()
    {
        return !throwing
                && !slashing
                && !stunning; // Iteration 5 ea
    }

    private bool OnSlope()
    {
        if (jumping) return false;
        
        RaycastHit hit;

        if(Physics.Raycast(transform.position, Vector3.down, out hit, controller.height/2 * slopeForceRayLength))
        {
            if (hit.normal != Vector3.up)
                return true;
        }
        return false;
    }
}
