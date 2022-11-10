
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

class Main4 : Story
{
    public override string Name => "Repair!";

    public override int StoryId => MakeID(true, 3);

    public override string Content => "Step closer to the RED facility";

    public Main4()
    {
        missList = new()
        {
            new Mission
            {
                setup = () => { },
                finalize = () => { GameObject.Find("TransparentWall_2").SetActive(false); },
                subTaskCheck = new()
                {
                    () => {
                        Debug.Log(null==GameObject.Find("Main3Trigger"));
                        Debug.Log(null==GameObject.Find("Main3Trigger").GetComponent<StoryTrigger>());
                        if(GameObject.Find("Main3Trigger").GetComponent<StoryTrigger>().isTriggered)
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