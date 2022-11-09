using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTrigger : MonoBehaviour
{
    public static TaskTrigger Instance; //Instantiating the TaskTrigger.cs

    public Task task;

    [SerializeField] public bool completePreTask = true;

    [SerializeField] public GameObject task1;

    void Awake()
    {
        Instance = this;
    }


    public void TriggerTask()      //start the conversation
    {
        TaskManager.Instance.StartTask(task);
    }

    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
      
            TaskManager.Instance.DisplayNextSentence();
        
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        task1.SetActive(false);
    }

    public void changeBool()
    {
        
    }
}
