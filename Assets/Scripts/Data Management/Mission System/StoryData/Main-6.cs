
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

class Main6 : Story
{
    public override string Name => "Fight!";

    public override int StoryId => MakeID(true, 3);

    public override string Content => "Go outside and kill 2 wolves";

    public Main6()
    {
        wolf1 = GameObject.Find("/Monster/Wolf1");
        wolf2 = GameObject.Find("/Monster/Wolf2");
        missList = new()
        {
            new Mission
            {
                setup = () => { },
                finalize = () => { },
                subTaskCheck = new()
                {
                    () => {
                        if(Input.GetMouseButtonDown(0))
                            missList[0].subTaskProgress[0] = 1.0f;
                    },
                    () => {
                        float tmp=0;
                        if(wolf1.GetComponent<BasicCombatant>().HitPoint<=0)
                            tmp += 0.5f;
                        if(wolf2.GetComponent<BasicCombatant>().HitPoint<=0)
                            tmp += 0.5f;
                        missList[0].subTaskProgress[1] = tmp;
                    },
                },
                subTaskProgress = new()
                {
                    0f, 0f,
                },
                subTaskText = new()
                {
                    "Shoot using left button",
                    "Aim and kill",
                },
            },
        };
    }
    private GameObject wolf1;
    private GameObject wolf2;

    private List<Mission> missList;
    public override List<Mission> MissList => missList;
    public override Story NextStory => new Main7();
}