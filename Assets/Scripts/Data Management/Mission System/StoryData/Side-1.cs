
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

class Side1 : Story
{
    public override string Name => "";

    public override int StoryId => MakeID(false, 1);

    public override string Content => "";

    public override List<Mission> MissList => new()
    {
       
    };

    public override Story NextStory => null;
}