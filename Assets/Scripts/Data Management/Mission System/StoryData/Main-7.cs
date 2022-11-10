
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

class Main7 : Story
{
    public override string Name => "Return!";

    public override int StoryId => MakeID(true, 3);

    public override string Content => "Fix and run";

    public Main7()
    {
        missList = new()
        {
            new Mission
            {
                setup = () => { },
                finalize = () => { },
                subTaskCheck = new()
                {
                },
                subTaskProgress = new()
                {
                },
                subTaskText = new()
                {
                    "Fix using Z on RED facility",
                    "Finish game using X on WHITE block",
                },
            },
        };
    }

    private List<Mission> missList;
    public override List<Mission> MissList => missList;
    public override Story NextStory => null;
}