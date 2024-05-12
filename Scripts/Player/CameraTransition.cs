using UnityEngine;
using UnityEngine.InputSystem;

namespace FMPUtils.Visuals.CameraTransition.Examples
{
    public class CameraTransition : MonoBehaviour
    {

        [Header("Essential Properties")]
        [SerializeField] private Camera fromCamera;
        [SerializeField] private Camera toCamera;
        [Range(0.1f, 10f)]
        [SerializeField] private float transitionDuration = 1.5f;
        [Header("User Defined Transition Properties")]
        [SerializeField] private Material transitionTestMaterial;
        [SerializeField] private Texture transitionTestMaskTexture;

        bool reassignAudioListenerToTargetCamera = true;
        public bool is_smelling = false;

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

    }
}
