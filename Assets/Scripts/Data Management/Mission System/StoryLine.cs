using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class StoryLine : MonoBehaviour
{
    private Story first = new Main1();
    private Story current;
    private List<Story> sides;
    // Start is called before the first frame update
    void Awake()
    {
        current = first;
        sides = new();
        GameState.storyLine = this;
    }

    void Start()
    {

        TaskManager.Instance.task_1_Text.text = current.Content;
        TaskManager.Instance.task_2_Text.text = current.Content;
        current.MissList[current.CurrentMission].setup();
    }

    public void AddSide(Story _Side)
    {
        sides.Add(_Side);
    }

    private void CheckMainMission()
    {
        if (current == null) return;
        if (current.CurrentMission >= current.MissList.Count) return;

        if (current.MissList[current.CurrentMission].Finished)
        {
            current.MissList[current.CurrentMission].finalize();
            current.CurrentMission++;
            if (current.CurrentMission >= current.MissList.Count)
            {
                current = current.NextStory;
                if (current != null)
                {
                    TaskManager.Instance.task_1_Text.text = current.Content;
                }
                else
                {
                    TaskManager.Instance.task_1_Text.text = "";
                    return;
                }
            }
            current.MissList[current.CurrentMission].setup();
        }
        else
        {
            foreach (var func in current.MissList[current.CurrentMission].subTaskCheck)
            {
                func();
            }
        }
    }

    private void CheckSideMissions()
    {
        for (int i = 0; i < sides.Count; i++)
        {
            if (sides[i].MissList[sides[i].CurrentMission].Finished)
            {
                sides[i].MissList[sides[i].CurrentMission].finalize();
                sides[i].CurrentMission++;
                if (sides[i].CurrentMission >= sides[i].MissList.Count)
                {
                    sides[i] = sides[i].NextStory;
                    if (sides[i] == null)
                    {
                        sides.RemoveAt(i);
                        TaskManager.Instance.task_1_Text.text = "";
                    }
                    else
                    {
                        TaskManager.Instance.task_1_Text.text = sides[i].Content;
                    }
                }
                sides[i].MissList[sides[i].CurrentMission].setup();
            }
            else
            {
                foreach (var func in sides[i].MissList[sides[i].CurrentMission].subTaskCheck)
                {
                    func();
                }
            }
        }
    }

    void UpdateMainSubTask()
    {
        if (current == null) { TaskManager.Instance.task_2_Text.text = ""; return; }
        string myText = current.Content;
        if (current.CurrentMission < current.MissList.Count)
            foreach (var str in current.MissList[current.CurrentMission].subTaskText)
            {
                myText += "\n";
                myText += str;
            }
        TaskManager.Instance.task_2_Text.text = myText;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMainMission();

        //CheckSideMissions();

        UpdateMainSubTask();
    }
}
