using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public static DialogueTrigger Instance; //Instantiating the DialogueTrigger.cs
    public Dialogue dialogue;
    private bool isTriggered = false;

    void Awake()
    {
        Instance = this;
    }

    public void TriggerDialogue()      //start the conversation
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void OnTriggerEnter(Collider other)     //Show button when triggered
    {
        UIManager.Instance.ShowStartTalkButton();
        isTriggered = true;
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HideStartTalkButton();
        isTriggered = false;
    }

    public void Update()
    {
        if (isTriggered && UIManager.Instance.InGameUIshowed && Input.GetKeyUp(KeyCode.F))
        {
            UIManager.Instance.StartTalk();
        }
        else if (!UIManager.Instance.InGameUIshowed && UIManager.Instance.ConversationOver && Input.GetKeyUp(KeyCode.F))  //如果谈话结束的话，则可以结束对话
        {
            UIManager.Instance.EndTalk();
        }
        else if (UIManager.Instance.ConversationStart && !isTriggered)
        {
            UIManager.Instance.EndTalk();
        }

        if (isTriggered && UIManager.Instance.ConversationStart && Input.GetKeyUp(KeyCode.C))
        {
            UIManager.Instance.ContinueTalk();
        }

    }
}
