using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    public float speed = 5f;
    public float maxSpeed = 5f;
    private bool isGrounded;
    //private bool canSprint = true;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    //below testing is run? and bool to check if run
    private KeyCode LeftShift = KeyCode.LeftShift;
    private bool isSprinting => /*canSprint &&*/ Input.GetKey(LeftShift);
    public float sprintSpeed = 10f;

   
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
      isGrounded = controller.isGrounded;

      
    }
    //receive the inputs for out InputManager.cs and apply them to char controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * (isSprinting ? sprintSpeed : speed) * Time.deltaTime);
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
