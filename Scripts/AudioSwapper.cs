using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwapper : MonoBehaviour
{
    [SerializeField]
    private AudioSource parkAudio, cityAudio;

    private void OnTriggerEnter(Collider other)
    {
        if(this.tag == "CityAudio")
        {
            parkAudio.enabled = false;
            cityAudio.enabled = true;
        }
    }
}
