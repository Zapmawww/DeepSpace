using System;
using System.Collections.Generic;

public class Mission
{
    /// <summary>
    /// The setup function that setup the scene or game state
    /// </summary>
    public Action setup;
    /// <summary>
    /// The finalize function that do clean job after a mission
    /// </summary>
    public Action finalize;
    /// <summary>
    /// The checkers that check the progress of each sub task
    /// </summary>
    public List<Action> subTaskCheck;
    /// <summary>
    /// The progress of each sub task
    /// </summary>
    public List<float> subTaskProgress;
    /// <summary>
    /// The description text of sub tasks
    /// </summary>
    public List<string> subTaskText;
    public bool Finished
    {
        get
        {
            foreach (var item in subTaskProgress)
            {
                if (item != 1.0f)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
public abstract class Story
{
    /// <summary>
    /// Internal make ID function
    /// </summary>
    /// <param name="_IsMain">Is this story a main story?</param>
    /// <param name="_ID">The ordinal number</param>
    /// <returns>The compiled ID</returns>
    static protected int MakeID(bool _IsMain, int _ID)
    {
        return _IsMain ? _ID : _ID << 16; //65535 is enough for this game
    }
    /// <summary>
    /// Story Name
    /// </summary>
    abstract public string Name { get; }
    /// <summary>
    /// Story Id
    /// </summary>
    abstract public int StoryId { get; }
    /// <summary>
    /// Story Content (Text description)
    /// </summary>
    abstract public string Content { get; }
    /// <summary>
    /// List of missions in this story
    /// </summary>
    abstract public List<Mission> MissList { get; }
    public int CurrentMission { get; set; } = 0;
    /// <summary>
    /// To determine the next story
    /// </summary>
    abstract public Story NextStory { get; }

}