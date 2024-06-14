using UnityEngine;

namespace FMPUtils.Visuals.CameraTransition.Examples
{
    public class TransitionEffectTester : MonoBehaviour
    {
        [Header("Essential Properties")]
        [SerializeField] private Camera fromCamera;
        [SerializeField] private Camera toCamera;
        [Range(0.1f, 10f)]
        [SerializeField] private float transitionDuration = 3f;
        [Header("User Defined Transition Properties")]
        [SerializeField] private Material transitionTestMaterial;
        [SerializeField] private Texture transitionTestMaskTexture;

        //[SerializeField] private KeyCode keyBuiltinAlphaFadeTransition = KeyCode.Space;


        private void Update()
        {
            bool wasTransitionTriggered = false;
            bool reassignAudioListenerToTargetCamera = true;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                CameraTransitionEffectController.Instance.ActivateTransition<AlphaFadeTransitionEffect>(fromCamera, toCamera, transitionDuration, reassignAudioListenerToTargetCamera);
                wasTransitionTriggered = true;
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                CameraTransitionEffectController.Instance.ActivateTransition<AlphaFadeTransitionEffect>(fromCamera, toCamera, transitionDuration, reassignAudioListenerToTargetCamera);
                wasTransitionTriggered = true;
            }


            if (wasTransitionTriggered)
                (fromCamera, toCamera) = (toCamera, fromCamera);
        }
    }
}

