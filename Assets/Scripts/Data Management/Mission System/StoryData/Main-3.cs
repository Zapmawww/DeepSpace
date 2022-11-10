
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

class Main3 : Story
{
    public override string Name => "Open the door";

    public override int StoryId => MakeID(true, 3);

    public override string Content => "Press G to open/close the door";

    public Main3()
    {
        door = GameObject.Find("first door");
        missList = new()
        {
            new Mission
            {
                setup = () => { },
                finalize = () => { },
                subTaskCheck = new()
                {
                    () => {
                        if(!door.activeInHierarchy)
                            missList[0].subTaskProgress[0] = 1.0f;
                    },
                    () => {
                        if(missList[0].subTaskProgress[0] == 1.0f && door.activeInHierarchy)
                            missList[0].subTaskProgress[1] = 1.0f;
                    },
                },
                subTaskProgress = new()
                {
                    0f, 0f,
                },
                subTaskText = new()
                {
                    "Open the door on the green block",
                    "Close the door on the green block outside",
                },
            },
        };
    }

    private GameObject door;
    private List<Mission> missList;
    public override List<Mission> MissList => missList;
    public override Story NextStory => new Main4();
}