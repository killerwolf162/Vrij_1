using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace FMPUtils.Visuals.CameraTransition.Examples
{
    public class CameraController : MonoBehaviour
    {

        [Header("Essential Properties")]
        [SerializeField] private Camera fromCamera;
        [SerializeField] private Camera toCamera;
        [Range(0.1f, 10f)]
        [SerializeField] private float transitionDuration = 1.5f;
        [Header("User Defined Transition Properties")]
        [SerializeField] private Material transitionTestMaterial;
        [SerializeField] private Texture transitionTestMaskTexture;
        private GameObject player;

        private List<Camera> camera_list = new List<Camera>();

        bool reassignAudioListenerToTargetCamera = true;
        public bool is_smelling = false;

        private Rigidbody rig;
        private CapsuleCollider col;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            rig = player.GetComponent<Rigidbody>();
            col = player.GetComponent<CapsuleCollider>();

            camera_list.AddRange(FindObjectsOfType<Camera>());
            camera_list.Count();
            camera_list.Reverse();

            foreach (Camera camera in camera_list)
            {
                camera.enabled = false;
                Debug.Log(camera.name);
            }

            fromCamera = camera_list[0];
            toCamera = camera_list[1];

            set_camera_active();
        }

        private void Update()
        {
            bool wasTransitionTriggered = false;
            bool reassignAudioListenerToTargetCamera = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CameraTransitionEffectController.Instance.ActivateTransition<AlphaFadeTransitionEffect>(fromCamera, toCamera, transitionDuration, reassignAudioListenerToTargetCamera);
                wasTransitionTriggered = true;
                is_smelling = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CameraTransitionEffectController.Instance.ActivateTransition<AlphaFadeTransitionEffect>(fromCamera, toCamera, transitionDuration, reassignAudioListenerToTargetCamera);
                wasTransitionTriggered = true;
                is_smelling = false;
            }


            if (wasTransitionTriggered)
                (fromCamera, toCamera) = (toCamera, fromCamera);
        }

        private void set_camera_active()
        {
            fromCamera.enabled = true;
        }

        private void set_camera_inactive()
        {
            fromCamera.enabled = false;
        }

        private void OnTriggerEnter(Collider other) // When adding new camera pair in scene add a new segment like below with corresponding tag. Example below:
        { 
            Debug.Log("hit");
            if(other.tag == "camera_1")
            {                                           // What the next segment would look like.
                set_camera_inactive();                 // set_camera_inactive();
                fromCamera = camera_list[0];           // fromCamera = camera_list[previous number segment number + 2]; 
                toCamera = camera_list[1];             // toCamera = camera_list[previous number segment number + 2]; 
                set_camera_active();                   // set_camera_active(); 
            }

            if (other.tag == "camera_2")
            {
                set_camera_inactive();
                fromCamera = camera_list[2];
                toCamera = camera_list[3];
                set_camera_active();
            }

            if (other.tag == "camera_3")
            {
                set_camera_inactive();
                fromCamera = camera_list[4];
                toCamera = camera_list[5];
                set_camera_active();
            }


        }



    }
}
