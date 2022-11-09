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

    private Queue<string> sentences;

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

        sentences.Clear();   //消除之前的任务
        //ConversationStart = true;
        //ConversationOver = false;


        foreach (string sentence in task.sentences)  //遍历字符串数组中的每个句子
        {
            sentences.Enqueue(sentence);   //将句子加入到队列中
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)   //判定是否已经达到队列的末尾
        {
            EndTask();
            return;
        }

        string sentence = sentences.Dequeue();   //调用队列中的下一个句子
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
