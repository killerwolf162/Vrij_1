using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Animator anim;
    private Rigidbody rig;
    public PlayerInput player_input;
    private ScentTracking scent;


    [SerializeField]
    private int walk_speed, sprint_speed;
    private Vector3 move;
    private bool is_walking;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        scent = GetComponent<ScentTracking>();
    }

    private void FixedUpdate()
    {
        Move(is_walking);
    }

    public void Move(bool sprinting)
    {
        if (scent.is_smelling == false)
        {
          if (sprinting == true)
              rig.transform.Translate(move * Time.deltaTime * sprint_speed);
          else if (sprinting != true)
              rig.transform.Translate(move * Time.deltaTime * walk_speed);
        }
    }

    public void OnMove(InputAction.CallbackContext input_value)
    {
        Vector2 movement = input_value.ReadValue<Vector2>();
        move = new Vector3(movement.x, 0, movement.y);
    }

    public void OnSprint(InputAction.CallbackContext input_value)
    {
        if (input_value.ReadValue<float>() > 0)
            is_walking = true;
        else
            is_walking = false;
    }


}
