using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColissionProxy : MonoBehaviour
{


        public Action<Collision> OnCollisionEnter_Action;
        public Action<Collision> OnCollisionStay_Action;
        public Action<Collision> OnCollisionExit_Action;

        private void OnCollisionEnter(Collision collision)
        {
            OnCollisionEnter_Action?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            OnCollisionStay_Action?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            OnCollisionExit_Action?.Invoke(collision);
        }

        // etc ...

}
