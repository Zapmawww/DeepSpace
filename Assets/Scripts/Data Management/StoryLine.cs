using System.Collections.Generic;

class Story
{
    /// <summary>
    /// Story Name
    /// </summary>
    public string Name { get; }
    /// <summary>
    /// Story Id
    /// </summary>
    public int StoryId { get; }
    /// <summary>
    /// Story Content
    /// </summary>
    public string Content { get; }
}

class StoryLine
{
    public List<Story> stories = new();
    Story currentStoryLevel = null;

}