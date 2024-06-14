using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfSpaceStopAnim : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private PlayerMovement move;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        move = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (move.enabled == false)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isWalking", false);
        }
    }
}
