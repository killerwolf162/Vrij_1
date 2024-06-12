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
        anim = GetComponent<Animator>();
        camera_controller = GameObject.FindWithTag("Camera_Controller").GetComponent<CameraController>();
    }

    private void FixedUpdate()
    {
        Move(is_walking);
    }

    public void Move(bool sprinting)
    {
        if (camera_controller.is_smelling == false)
        {
            

            if (movementDirection != Vector3.zero)
            {
                if (sprinting == true)
                    rig.transform.Translate(Vector3.forward * Time.deltaTime * sprint_speed);
                else if (sprinting != true)
                    rig.transform.Translate(Vector3.forward * Time.deltaTime * walk_speed);

                //if(movementDirection == Vector3.up)
                //{
                //    Quaternion to_rotation = Quaternion.LookRotation(movementDirection, Vector3.forward);
                //    transform.rotation = Quaternion.RotateTowards(transform.rotation, to_rotation, rotation_speed * Time.deltaTime);
                //}
                if (movementDirection == Vector3.left)
                {
                    Quaternion to_rotation = Quaternion.LookRotation(movementDirection, Vector3.left);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, to_rotation, rotation_speed * Time.deltaTime);
                }
                else if (movementDirection == Vector3.right)
                {
                    Quaternion to_rotation = Quaternion.LookRotation(movementDirection, Vector3.right);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, to_rotation, rotation_speed * Time.deltaTime);
                }
                else if (movementDirection == Vector3.back)
                {
                    Quaternion to_rotation = Quaternion.LookRotation(movementDirection, Vector3.back);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, to_rotation, rotation_speed * Time.deltaTime);
                }
                
            }              
        }
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
