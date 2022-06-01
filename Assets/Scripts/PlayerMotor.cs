using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public float maxSpeed = 5f;
    private bool isGrounded;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    public GameObject Character;

    //bool to check if run
    private KeyCode LeftShift = KeyCode.LeftShift;
    private bool isSprinting => Input.GetKey(LeftShift);
    public float sprintSpeed = 10f;


    Animator animator;





    // Start is called before the first frame update
    void Start()
    {
        //get animator 
        animator = Character.GetComponent<Animator>();

        //get char controller 
        controller = GetComponent<CharacterController>();

        //animatorHandler = GetComponentInChildren<AnimatorHandler>();
        animator.GetFloat("Speed");

        //animator = GetComponent<animator>;




    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;



        //animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0);

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && Input.GetKey(KeyCode.LeftShift))
        {
            //(string name, float value, float dampTime, deltaTime)
            animator.SetFloat("Speed", 1f, .1f, Time.deltaTime);
        }

        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            animator.SetFloat("Speed", 0.5f, .1f, Time.deltaTime);



        }

        else
        {
            animator.SetFloat("Speed", 0f, .1f, Time.deltaTime);





        }
    }

    //receive the inputs for out InputManager.cs and apply them to char controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        //move char controller by trans(plyr inpt)x(bool value is sprinting?sprintelse)x(deltatime)
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
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}

 
