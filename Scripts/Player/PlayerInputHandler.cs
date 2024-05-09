using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerMovement player_movement;
    private ScentTracking scent_tracking;
    

    public void OnMove(InputAction.CallbackContext input)
    {
        player_movement.OnMove(input);
    }

    public void OnSprint(InputAction.CallbackContext input)
    {
        player_movement.OnSprint(input);
    }

    public void OnSmell(InputAction.CallbackContext input)
    {
        scent_tracking.OnSmell(input);
    }
}
