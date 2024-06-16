using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractable : MonoBehaviour, IInteractable {

    [SerializeField] private string interactText;
    [SerializeField] private TalkEventManager talkManager;

    private void Awake()
    {
        talkManager = GameObject.FindObjectOfType<TalkEventManager>();
    }
    public void Interact() {

        //animator.SetTrigger("Talk");

        if(this.CompareTag("Family") == true)
        {
            talkManager.interactWithFamily.Invoke();
        }
        if (this.CompareTag("Museum") == true)
        {
            talkManager.enterMusuem.Invoke();
        }
        else
        {
            Debug.Log("not the tag");
        }
    }

    public string GetInteractText() {
        return interactText;
    }

    public Transform GetTransform() {
        return transform;
    }

}