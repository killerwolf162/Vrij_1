using FMPUtils.Visuals.CameraTransition.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rig;
    public PlayerInput player_input;
    private CameraController camera_controller;


    [SerializeField]
    private int walk_speed, sprint_speed;
    private Vector3 movementDirection;
    private bool is_walking;
    public float rotation_speed;



    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        //camera_controller = GameObject.FindWithTag("Camera_Controller").GetComponent<CameraController>();
    }

    private void FixedUpdate()
    {
        Move(is_walking);
    }

    public void Move(bool sprinting)
    {

        if (movementDirection == Vector3.zero)
        {
            anim.SetBool("isRunning", false);
            anim.SetBool("isWalking", false);
            anim.SetBool("isCrouching", true);
        }
        if (movementDirection != Vector3.zero)
        {
            if(movementDirection.z > 0)
            {
                if (sprinting == true)
                {
                    rig.transform.Translate(Vector3.forward * Time.deltaTime * sprint_speed);
                    anim.SetBool("isRunning", true);
                }
                if (sprinting != true)
                {
                    rig.transform.Translate(Vector3.forward * Time.deltaTime * walk_speed);
                    anim.SetBool("isWalking", true);
                }

            }
            if (movementDirection.z < 0)
            {
                if (sprinting == true)
                {
                    rig.transform.Translate(Vector3.back * Time.deltaTime * sprint_speed);
                    anim.SetBool("isRunning", true);
                }
            }
            if (movementDirection.x > 0)
            {
                turnRight();
            }
            if (movementDirection.x < 0)
            {
                turnLeft();
            }

        }



        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetBool("isCrouching", true);
        }
        else
            anim.SetBool("isCrouching", false);

    }

    private void goForward()
    {

    }
    private void turnRight()
    {
        float angle = rotation_speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
    }

    private void turnLeft()
    {
        float angle = -rotation_speed * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.up);
    }

    public void OnMove(InputAction.CallbackContext input_value)
    {
        Vector2 movement = input_value.ReadValue<Vector2>();
        movementDirection = new Vector3(movement.x, 0, movement.y);
        movementDirection.Normalize();
    }

    public void OnSprint(InputAction.CallbackContext input_value)
    {
        if (input_value.ReadValue<float>() > 0)
            is_walking = true;
        else
            is_walking = false;
    }


}
