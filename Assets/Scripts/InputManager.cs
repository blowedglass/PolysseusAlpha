using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class InputManager : MonoBehaviour
    {

        private PlayerInput playerInput;
        private PlayerInput.OnFootActions onFoot;


        private PlayerMotor motor;
        private PlayerLook look;

        public bool b_input;
        public bool rb_Input;
        public bool rt_Input;

        PlayerAttacker playerAttacker;
        PlayerInventory playerInventory;


        // Start is called before the first frame update
        void Awake()
        {
            playerInput = new PlayerInput();
            onFoot = playerInput.OnFoot;

            motor = GetComponent<PlayerMotor>();
            look = GetComponent<PlayerLook>();
            // below- permformed, started,canceled.  ctx=> is a reference back to the onfoot jump
            onFoot.Jump.performed += ctx => motor.Jump();

            playerAttacker = GetComponent<PlayerAttacker>();
            playerInventory = GetComponent<PlayerInventory>();
        }

        void FixedUpdate()
        {
            //tell playermotor to move using values from movement action.
            motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        }
        void LateUpdate()
        {
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

        }

        private void OnEnable()
        {
            onFoot.Enable();
        }

        private void OnDisable()
        {
            onFoot.Disable();
        }

        public void TickUpdate(float delta)
        {
            HandleAttackInput(delta);

        }


        private void HandleAttackInput(float delta)
        {
            playerInput.OnFoot.RB.performed += i => rb_Input = true;
            playerInput.OnFoot.RT.performed += i => rb_Input = true;

            //RB Input handles the RIGHT hand weapon's light attack
            if (rb_Input)
            {
                playerAttacker.HandleLightAttack(playerInventory.rightWeapon);
            }

            if (rt_Input)
            {
                playerAttacker.HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }
        



        //test to change unity file for git
    }
}
