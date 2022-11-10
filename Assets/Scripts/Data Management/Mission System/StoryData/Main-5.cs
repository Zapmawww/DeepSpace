
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

class Main5 : Story
{
    public override string Name => "Grab!";

    public override int StoryId => MakeID(true, 3);

    public override string Content => "Collect using F";

    public Main5()
    {
        missList = new()
        {
            new Mission
            {
                setup = () => { },
                finalize = () => { },
                subTaskCheck = new()
                {
                    () => {
                        if(UIManager.Instance.pickUpox)
                            missList[0].subTaskProgress[0] = 1.0f;
                    },
                },
                subTaskProgress = new()
                {
                    0f,
                },
                subTaskText = new()
                {
                    "Collect oxygen tank",
                },
            },
            new Mission
            {
                setup = () => { },
                finalize = () => { },
                subTaskCheck = new()
                {
                    () => {
                        if(UIManager.Instance.pickUp001)
                            missList[1].subTaskProgress[0] = 1.0f;
                    },
                    () => {
                        if(UIManager.Instance.pickUp002)
                            missList[1].subTaskProgress[1] = 1.0f;
                    },
                },
                subTaskProgress = new()
                {
                    0f, 0f,
                },
                subTaskText = new()
                {
                    "Collect material 1",
                    "Collect material 2",
                },
            },
        };
    }

    private List<Mission> missList;
    public override List<Mission> MissList => missList;
    public override Story NextStory => new Main6();
}