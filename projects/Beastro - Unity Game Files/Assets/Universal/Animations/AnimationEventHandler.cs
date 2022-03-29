using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEventHandler : MonoBehaviour
{
    private AudioSource stunAbilitySound;
    private HuntingPlayerController huntingPlayerController;
    private FootstepHandler footstepHandler;
    private PlayerHealth playerhealth;
    public ParticleSystem psStunAOE;
    private StunAOE stunAOEscr;       

    private void Start()
    {
        huntingPlayerController = transform.parent.gameObject.GetComponent<HuntingPlayerController>();
        footstepHandler = transform.parent.GetChild(1).gameObject.GetComponent<FootstepHandler>();
        playerhealth = GetComponent<PlayerHealth>();
        stunAOEscr = transform.parent.gameObject.GetComponent<StunAOE>();        
    }

    // Iteration 3
    public void FinishedStun()
    {
        if (huntingPlayerController != null)
            huntingPlayerController.endStun();
    }

    public void FinishedDodge()
    {
        if (huntingPlayerController != null)
            huntingPlayerController.endDodge();
    }

    // Iteration 3
    public void FinishedSlash()
    {
        if (huntingPlayerController != null)
            huntingPlayerController.endSlash();
    }
    
    // Iteration 3
    public void FinishedThrow()
    {
        if (huntingPlayerController != null)
            huntingPlayerController.endThrow();
    }

    public void PlayFootstepSound()
    {
        if (footstepHandler != null)
            footstepHandler.playFootstepSound();
    }

    // Iteration 3
    public void spawnProjectile()
    {
        WeaponInventory.useSecondary();
    }
    
    // Iteration 3 ea
    public void slashHit()
    {        
        WeaponInventory.usePrimary();
    }

    // Iteration 3 ea
    public void slashSound()
    {
        transform.parent.GetChild(3).GetComponent<AudioSource>().Play();
    }

    public void gotHitEnd()
    {
        huntingPlayerController.takingDamage = false;
    }

    public void stunSound()
    {
        transform.parent.GetChild(8).GetComponent<AudioSource>().Play();
        transform.parent.GetChild(9).GetComponent<AudioSource>().Play();
    }

    public void StunAOE()
    {
        stunAOEscr.useAbility();
        psStunAOE.Play(true);
    }

    public void Death()
    {
        SceneManager.LoadScene("Scenes/Restaurant");
    }
}
