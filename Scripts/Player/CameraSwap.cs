using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwap : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera normalCamera, smellCamera;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        normalCamera = GameObject.FindGameObjectWithTag("Player_camera").GetComponent<Camera>();
        smellCamera = GameObject.FindGameObjectWithTag("Smell_camera").GetComponent<Camera>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            normalCamera.enabled = false;
            smellCamera.enabled = true;
            playerMovement.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            normalCamera.enabled = true;
            smellCamera.enabled = false;
            playerMovement.enabled = true;
        }
    }
}
