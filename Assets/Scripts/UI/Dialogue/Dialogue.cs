using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue 
{
    public string name;   //进行对话的npc的名字

    [TextArea(3,10)]
    public string[] sentences;     //储存要进行的对话
}
