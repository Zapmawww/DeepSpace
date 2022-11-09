using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance; //Instantiating the DialogueManager.cs

    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Queue<string> sentences;

    [SerializeField] public bool ConversationStart = false;    //Determining if a conversation is over
    [SerializeField] public bool ConversationOver = false;    //Determining if a conversation is over



    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with "+ dialogue.name);
        nameText.text = dialogue.name;

        sentences.Clear();   //消除之前的对话
        ConversationStart = true;
        ConversationOver = false;


        foreach (string sentence in dialogue.sentences)  //遍历字符串数组中的每个句子
        {
            sentences.Enqueue(sentence);   //将句子加入到队列中
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)   //判定是否已经达到队列的末尾
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();   //调用队列中的下一个句子
        dialogueText.text = sentence;
    }

    public void EndDialogue()
    {
        Debug.Log("End of conversation");  
        ConversationStart = false;
        ConversationOver = true;
    }

    public void changeBool()
    {
        ConversationOver = false;
    }

}
