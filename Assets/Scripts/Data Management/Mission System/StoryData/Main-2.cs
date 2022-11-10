
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

class Main2 : Story
{
    public override string Name => "First Talk";

    public override int StoryId => MakeID(true, 2);

    public override string Content => "Press F to talk with AI";

    public Main2()
    {
        missList = new()
        {
            new Mission
            {
                setup = () => {Resources.FindObjectsOfTypeAll<GameObject>().FirstOrDefault(g=>g.name=="Conversation1").SetActive(true); },
                finalize = () => { },
                subTaskCheck = new()
                {
                    () => {
                        if(UIManager.Instance.ConversationStart)
                            missList[0].subTaskProgress[0] = 1.0f;
                    },
                },
                subTaskProgress = new()
                {
                    0f,
                },
                subTaskText = new()
                {
                    "Stand on the white block to trigger talk"
                },
            },
        };
    }

    private List<Mission> missList;
    public override List<Mission> MissList => missList;
    public override Story NextStory => new Main3();
}