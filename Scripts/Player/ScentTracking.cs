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

    private CameraController camera_controller;
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



}
