
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

class Main4 : Story
{
    public override string Name => "Repair!";

    public override int StoryId => MakeID(true, 4);

    public override string Content => "Step closer to the RED facility";

    public Main4()
    {
        missList = new()
        {
            new Mission
            {
                setup = () => { },
                finalize = () => { 
                    GameObject.Find("TransparentWall_2").SetActive(false);
                    GameObject.Find("Main3Trigger").SetActive(false);
                },
                subTaskCheck = new()
                {
                    () => {
                        if(GameObject.Find("Main3Trigger").GetComponent<StoryTrigger>().IsTriggered)
                            missList[0].subTaskProgress[0] = 1.0f;
                    },
                },
                subTaskProgress = new()
                {
                    0f,
                },
                subTaskText = new()
                {
                    "Check its information",
                },
            },
        };
    }

    private List<Mission> missList;
    public override List<Mission> MissList => missList;
    public override Story NextStory => new Main5();
}