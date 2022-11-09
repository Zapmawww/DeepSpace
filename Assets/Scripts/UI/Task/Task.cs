using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task 
{
    [TextArea(3, 10)]
    public string[] sentences;     //Storage of tasks to be carried out          
}
