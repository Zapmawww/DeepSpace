using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance; //Instantiating the TaskManager.cs
    

    public TMP_Text task_1_Text;
    public TMP_Text task_2_Text;

    public Queue<string> sentences;

    [SerializeField] public bool taskStart = false;    //Determining if a task is over
    [SerializeField] public bool taskOver = false;    //Determining if a task is over



    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartTask(Task task)
    {

        sentences.Clear();   //����֮ǰ������
        //ConversationStart = true;
        //ConversationOver = false;


        foreach (string sentence in task.sentences)  //�����ַ��������е�ÿ������
        {
            sentences.Enqueue(sentence);   //�����Ӽ��뵽������
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)   //�ж��Ƿ��Ѿ��ﵽ���е�ĩβ
        {
            EndTask();
            return;
        }

        string sentence = sentences.Dequeue();   //���ö����е���һ������
        Debug.Log("next task");
        task_1_Text.text = sentence;
        task_2_Text.text = sentence;
    }

    public void EndTask()
    {
        Debug.Log("End of task");
        //ConversationStart = false;
        //ConversationOver = true;
    }

    public void changeBool()
    {
        taskStart = true;
    }

   
}
