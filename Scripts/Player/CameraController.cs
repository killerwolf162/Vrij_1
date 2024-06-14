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
        private List<Camera> normal_camera_list = new List<Camera>();
        private List<Camera> smell_camera_list = new List<Camera>();

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

            foreach (Camera camera in camera_list)
            {
                if (camera.tag == "Smell_camera")
                {
                    smell_camera_list.Add(camera);
                    
                }
                if (camera.tag != "Smell_camera")
                {
                    normal_camera_list.Add(camera);
                }
                camera.enabled = false;
            }


            smell_camera_list = smell_camera_list.OrderBy(o => o.name).ToList();
            normal_camera_list = normal_camera_list.OrderBy(o => o.name).ToList();

            foreach (Camera camera in normal_camera_list)
            {
                Debug.Log(camera.name);
            }
            foreach (Camera camera in smell_camera_list)
            {
                Debug.Log(camera.name);
            }

            fromCamera = normal_camera_list[0];
            toCamera = smell_camera_list[0];

            set_camera_active();
        }

        private void Update()
        {
            bool wasTransitionTriggered = false;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CameraTransitionEffectController.Instance.ActivateTransition<AlphaFadeTransitionEffect>(fromCamera, toCamera, transitionDuration);
                wasTransitionTriggered = true;
                is_smelling = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CameraTransitionEffectController.Instance.ActivateTransition<AlphaFadeTransitionEffect>(fromCamera, toCamera, transitionDuration);
                wasTransitionTriggered = true;
                is_smelling = false;
            }


            if (wasTransitionTriggered)
                (fromCamera, toCamera) = (toCamera, fromCamera);

            this.transform.position = this.transform.parent.position;
        }

        private void set_camera_active()
        {
            fromCamera.enabled = true;
        }

        private void set_camera_inactive()
        {
            fromCamera.enabled = false;
        }

        //private void OnTriggerEnter(Collider other) // When adding new camera pair in scene add a new segment like below with corresponding tag. Example below:
        //{ 
        //    Debug.Log("hit");
        //    if(other.tag == "camera_1")
        //    {                                           // What the next segment would look like.
        //        set_camera_inactive();                 // set_camera_inactive();
        //        fromCamera = normal_camera_list[0];           // fromCamera = normal_camera_list[previous number segment number + 1]; 
        //        toCamera = smell_camera_list[0];            // toCamera = smell_camera_list[previous number segment number +1]; 
        //        set_camera_active();                   // set_camera_active(); 
        //    }

        //    if (other.tag == "camera_2")
        //    {
        //        set_camera_inactive();
        //        fromCamera = normal_camera_list[1];
        //        toCamera = smell_camera_list[1];
        //        set_camera_active();
        //    }

        //    if (other.tag == "camera_3")
        //    {
        //        set_camera_inactive();
        //        fromCamera = normal_camera_list[2];
        //        toCamera = smell_camera_list[2];
        //        set_camera_active();
        //    }

        //    if (other.tag == "camera_4")
        //    {
        //        set_camera_inactive();
        //        fromCamera = normal_camera_list[3];
        //        toCamera = smell_camera_list[3];
        //        set_camera_active();
        //    }

        //    if (other.tag == "camera_5")
        //    {
        //        set_camera_inactive();
        //        fromCamera = normal_camera_list[4];
        //        toCamera = smell_camera_list[4];
        //        set_camera_active();
        //    }

        //    if (other.tag == "camera_6")
        //    {
        //        set_camera_inactive();
        //        fromCamera = normal_camera_list[5];
        //        toCamera = smell_camera_list[5];
        //        set_camera_active();
        //    }

        //    if (other.tag == "camera_7")
        //    {
        //        set_camera_inactive();
        //        fromCamera = normal_camera_list[6];
        //        toCamera = smell_camera_list[6];
        //        set_camera_active();
        //    }

        //    if (other.tag == "camera_8")
        //    {
        //        set_camera_inactive();
        //        fromCamera = normal_camera_list[7];
        //        toCamera = smell_camera_list[7];
        //        set_camera_active();
        //    }

        //    if (other.tag == "camera_9")
        //    {
        //        set_camera_inactive();
        //        fromCamera = normal_camera_list[8];
        //        toCamera = smell_camera_list[8];
        //        set_camera_active();
        //    }

        //    if (other.tag == "camera_10")
        //    {
        //        set_camera_inactive();
        //        fromCamera = normal_camera_list[9];
        //        toCamera = smell_camera_list[9];
        //        set_camera_active();
        //    }

        //}



    }
}
