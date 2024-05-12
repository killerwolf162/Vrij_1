using FMPUtils.Visuals.CameraTransition.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScentTracking : MonoBehaviour
{

    //This script is meant so the player can see the scent trail he needs to follow to get towards the objective.
    // For the scent trail im planning to use the Unity Particle system to make a trail of clouds the player can follow
    // This ability will have a max duration before it is canceled, the player will also be immobile while using it

    private CameraTransition camera_controller;
    [SerializeField] public float scent_timer = 5; // link timer to a slider in the UI

    public List<ParticleSystem> particle_systems = new List<ParticleSystem>();


    private void Awake()
    {
        particle_systems.AddRange(Resources.FindObjectsOfTypeAll<ParticleSystem>());
    }

    private void Start()
    {
        Debug.Log(particle_systems.Count);
        foreach (ParticleSystem system in particle_systems)
        {
            system.Play();
        }
    }

    private void FixedUpdate()
    {
        if (scent_timer < 5 && camera_controller.is_smelling == false) // resets scent timer after it was used to max value overtime
        {
            scent_timer += Time.deltaTime;
        }
        else if (scent_timer > 5 && camera_controller.is_smelling == false)
       {
            scent_timer = 5;
        }

    }
/*
    public void OnSmell(InputAction.CallbackContext input_value)
    {
        if (input_value.ReadValue<float>() > 0 && scent_timer > 1) // while scent button is pressed and timer is above 0 do the thing
        {
            StartCoroutine(Smelling(input_value));
        }
    }

    private IEnumerator Smelling(InputAction.CallbackContext input_value)
    {
        is_smelling = true; // disables movement in movement script

        while (is_smelling == true)
        {
            //smell_camera.enabled = true;
            //player_camera.enabled = false;
            scent_timer -= Time.deltaTime;

            //give screen a blue overlay/shader, assassin's creed esc. something with shaders etc. (https://www.youtube.com/watch?v=7_H0b82y_qU)

            yield return null;

            if (input_value.ReadValue<float>() <= 0 || scent_timer <= 0) // if timer hits 0 player cant use the ability anymore
            {
                is_smelling = false;
                //smell_camera.enabled = false;
                //player_camera.enabled = true;
                //Stop_playing_particels();
                StopCoroutine(Smelling(input_value));
            }
        }

    }
*/


}
