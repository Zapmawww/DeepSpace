using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger Instance; //Instantiating the DialogueTrigger.cs
    public Dialogue dialogue;

    void Awake()
    {
        Instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.ShowStartTalkButton();
    }

    public void OnTriggerExit(Collider other)
    {
        UIManager.Instance.HideStartTalkButton();
    }
}
