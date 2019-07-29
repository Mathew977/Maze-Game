using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

[System.Flags]
public enum CharacterStates
{
    Idle = 0,
    Walking = 1,
    Running = 2,
    Jumping = 4,
    JumpingAndWalking = 5,
    JumpingAndRunning = 6,
    Attacking = 8,
    WalkingAndAttacking = 9,
    Reloading = 16,
    WalkingAndReloading = 17,
    Aiming = 32,
    AimingAndWalking = 33,
    AimingAndAttacking = 40,
    Dead = 64,
}

public class FPCharacter : MonoBehaviour {
    // Use this for initialization
    private Animator animator;
    private FirstPersonController fpc;
    private CharacterStates characterState;
    private bool isAiming;
    private bool isReloading;
    private bool isAttacking;
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (fpc == null)
            return;
        if (fpc.IsJumping == false)
        {
            isAiming = Input.GetKey(KeyCode.E);

            isReloading = Input.GetKey(KeyCode.R);

            isAttacking = Input.GetMouseButtonDown(0);
        }
    }

    private void UpdateAnimator()
    {
        if (animator == null)
            return;

        if (fpc == null)
            return;

        if (fpc.IsJumping == false)
        {
            if (isAiming == true)
            {
                if (fpc.IsMoving == true)
                {
                    characterState = CharacterStates.AimingAndWalking;
                }
                else
                {
                    if (isAttacking == true)
                    {
                        characterState = CharacterStates.AimingAndAttacking;
                    }
                    else
                    {
                        characterState = CharacterStates.Aiming;
                    }
                }
            }
            else
            {
                if (fpc.IsMoving == true)
                {
                    if (fpc.IsWalking == true)
                    {
                        if (isReloading == true)
                        {
                            characterState = CharacterStates.WalkingAndReloading;
                            GameObject.Find("BulletCreator").GetComponent<BcWeapon>().currentAmmo = 10;
                        }
                        else
                        {
                            characterState = CharacterStates.Walking;
                        }
                    }
                    else if (fpc.IsRunning == true)
                    {
                        characterState = CharacterStates.Running;
                    }
                    else
                    {
                        characterState = CharacterStates.WalkingAndAttacking;
                    }
                }
                else
                {
                    if (isAttacking == true)
                    {
                        characterState = CharacterStates.Attacking;
                    }
                    else
                    {
                        if (isReloading == true)
                        {
                            characterState = CharacterStates.Reloading;
                            GameObject.Find("BulletCreator").GetComponent<BcWeapon>().currentAmmo = 10;
                        }
                        else
                        {
                            characterState = CharacterStates.Idle;
                        }
                    }
                }
                
            }
        }
        else
        {
            if (fpc.IsMoving == true)
            {
                if (fpc.IsWalking == true)
                {
                    characterState = CharacterStates.JumpingAndWalking;
                }
                else
                {
                    characterState = CharacterStates.JumpingAndRunning;
                }
            }
            
        }

        //Update Animator Parameters
        animator.SetInteger("CharacterState", (int)characterState);
        animator.SetFloat("Speed", fpc.CurrentSpeed);
    }

    private void Awake()
    {
        //Get the FirstPersonController script component attached to the parent game object this script is attached to
        fpc = transform.parent.gameObject.GetComponent<FirstPersonController>();

        //Get the animator component attached to the game object this script is attached to
        animator = transform.gameObject.GetComponent<Animator>();
    }

    private void OnAnimatorMove()
    {
        UpdateAnimator();
    }
}