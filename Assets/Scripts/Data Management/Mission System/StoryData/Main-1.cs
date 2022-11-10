
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

class Main1 : Story
{
    public override string Name => "Startup";

    public override int StoryId => MakeID(true, 1);

    public override string Content => "Try basic operations, T for details";

    public Main1()
    {
        missList = new()
        {
            new Mission
            {
                setup = () => { },
                finalize = () => { GameObject.Find("TransparentWall_1").SetActive(false); },
                subTaskCheck = new()
                {
                    () => {
                        if((Player.Instance.transform.position-new Vector3(19.24f, 2f, 12.97f)).magnitude>10)
                        {
                            missList[0].subTaskProgress[0] = 1.0f;
                        }
                    },
                    () => {
                        if(Input.GetKeyDown(KeyCode.Space))
                            missList[0].subTaskProgress[1] = 1.0f;
                    },
                },
                subTaskProgress = new()
                {
                    0f, 0f
                },
                subTaskText = new()
                {
                    "Try to move around using WASD",
                    "Try to jump using Space",
                },
            },
        };
    }

    private List<Mission> missList;
    public override List<Mission> MissList => missList;
    public override Story NextStory => new Main2();

}