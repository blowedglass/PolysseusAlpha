using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SG
{
    public class PlayerMotor : MonoBehaviour
    {
        [Header("Character Essentials")]
        private CharacterController controller;
        public GameObject Character;
        Animator animator;
        InputManager inputManager;
        public AnimatorHandler animatorHandler;

        [Header("Movement Speeds")]
        private Vector3 playerVelocity;
        public float speed = 5f;
        public float maxSpeed = 5f;

        [Header("Jump Functionality")]
        private bool isGrounded;
        public float gravity = -9.8f;
        public float jumpHeight = 3f;
        public float fallMultiplier = 2.5f;
        public float lowJumpMult = 2f;

        [Header("Sprint Functionality")]
        private KeyCode LeftShift = KeyCode.LeftShift;
        private bool isSprinting => Input.GetKey(LeftShift);
        public float sprintSpeed = 10f;

        // Start is called before the first frame update
        void Start()
        {
            //get animator 
            animator = Character.GetComponent<Animator>();

            //get char controller 
            controller = GetComponent<CharacterController>();

            //
            animator.GetFloat("Speed");

            animatorHandler = GetComponentInChildren<AnimatorHandler>();
           

        }

        // Update is called once per frame
        void Update()
        {
            //GROUNDED built in controller method
            isGrounded = controller.isGrounded;

            //SPRINTING ANIMATOR
            if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
            {
                animator.SetFloat("Speed", 1f, .1f, Time.deltaTime);
            }

            //WALKING ANIMATOR
            else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                animator.SetFloat("Speed", 0.5f, .1f, Time.deltaTime);
            }

            //IDLE ANIMATOR
            else
            {
                animator.SetFloat("Speed", 0f, .1f, Time.deltaTime);
            }

            //JUMP CONTROLLER
            if (playerVelocity.y < 0)
            {
                playerVelocity.y += Vector3.up.y * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (playerVelocity.y > 0 && !Input.GetButton("Jump"))
            {
                playerVelocity.y += Vector3.up.y * Physics.gravity.y * (lowJumpMult - 1) * Time.deltaTime;
            }

        }

        private void LateUpdate()
        {
            inputManager.rb_Input = false;
            inputManager.rt_Input = false;
        }

        //receive the inputs for out InputManager.cs and apply them to char controller
        public void ProcessMove(Vector2 input)
        {
            Vector3 moveDirection = Vector3.zero;
            moveDirection.x = input.x;
            moveDirection.z = input.y;

            //WALKING/SPRINTING
            //MOVES CHARACTER CONTROLLER OBJECT USING KEYBOARD INPUT, TERNARY OPERATOR USED TO DETERMINE WALKING OR SPRINTING SPEED
            controller.Move(transform.TransformDirection(moveDirection) * (isSprinting ? sprintSpeed : speed) * Time.deltaTime);
            //gravity and keeping player to ground when appropriate 
            playerVelocity.y += gravity * Time.deltaTime;
            if (isGrounded && playerVelocity.y < 0)
                playerVelocity.y = -2f;
            controller.Move(playerVelocity * Time.deltaTime);
            Debug.Log(playerVelocity.y);
        }

        public void Jump()
        {
            if (isGrounded)
            {
                playerVelocity.y = Vector3.up.y * jumpHeight;
            }
        }
    }
}