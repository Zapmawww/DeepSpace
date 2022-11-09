
using System;
using System.Collections.Generic;

/// <summary>
/// Combat System is a action and log system that regulates the combat action input and log outcome
/// </summary>
static class CombatSystem
{
    /// <summary>
    /// ComabatLog class contains the basic structure of a combat log, useful for log analysing
    /// </summary>
    public class ComabatLog
    {
        public BasicCombatant source;
        public CombatMessenger cm;
        public BasicCombatant target;
        public string extraInfo;
        public DateTime time;
        public bool succeeded;
    }
    /// <summary>
    /// contains all logs
    /// </summary>
    static public List<ComabatLog> Logs { get; private set; } = new();
    /// <summary>
    /// logs for a given target, useful for analysing
    /// </summary>
    static public Dictionary<BasicCombatant, List<ComabatLog>> AsTarget { get; private set; } = new();
    /// <summary>
    /// logs for a given source, useful for analysing
    /// </summary>
    static public Dictionary<BasicCombatant, List<ComabatLog>> AsSource { get; private set; } = new();
    /// <summary>
    /// clean all combat logs, e.g. after scene switching
    /// </summary>
    static public void Clean()
    {
        AsTarget.Clear();
        AsSource.Clear();
        Logs.Clear();
    }
    /// <summary>
    /// Add a combat action to perform and log
    /// </summary>
    /// <param name="_Source">The source combatant who starts the action</param>
    /// <param name="_Target">The target combatant who receives the action</param>
    /// <param name="_CM">The commbatant messenger which contains the action data in different types</param>
    /// <param name="_Info">Extra string info that helpful for logs</param>
    /// <returns>If the action is valid (successfully performed)</returns>
    static public bool AddCombatAct(BasicCombatant _Source, BasicCombatant _Target, CombatMessenger _CM, string _Info)
    {
        //act
        var ret = _Target.ReceiveCM(_CM);

        //log
        var log = new ComabatLog { source = _Source, target = _Target, cm = _CM, extraInfo = _Info, time = DateTime.Now, succeeded = ret };
        Logs.Add(log);

        if (!AsTarget.ContainsKey(_Target))
            AsTarget.Add(_Target, new());
        AsTarget[_Target].Add(log);

        if (!AsSource.ContainsKey(_Source))
            AsSource.Add(_Source, new());
        AsSource[_Source].Add(log);

        return ret;
    }
    /// <summary>
    /// Analyse the combat logs for a given target, using a reducer
    /// </summary>
    /// <typeparam name="RetT">Return type of the reducer</typeparam>
    /// <param name="_Target">Target combatant</param>
    /// <param name="_Pred">The reducer (acc, log) => ret</param>
    /// <returns>The result from the refucer</returns>
    static public RetT TargetLogAnalyse<RetT>(BasicCombatant _Target, Func<RetT, ComabatLog, RetT> _Pred) where RetT : new()
    {
        RetT ret = new();
        foreach (var log in AsTarget[_Target])
        {
            ret = _Pred(ret, log);
        }
        return ret;
    }
    /// <summary>
    /// Analyse the combat logs for a given source, using a reducer
    /// </summary>
    /// <typeparam name="RetT">Return type of the reducer</typeparam>
    /// <param name="_Source">Source combatant</param>
    /// <param name="_Pred">The reducer (acc, log) => ret</param>
    /// <returns>The result from the refucer</returns>
    static public RetT SourceLogAnalyse<RetT>(BasicCombatant _Source, Func<RetT, ComabatLog, RetT> _Pred) where RetT : new()
    {
        RetT ret = new();
        foreach (var log in AsSource[_Source])
        {
            ret = _Pred(ret, log);
        }
        return ret;
    }
    /// <summary>
    /// Analyse all combat logs in the system, using a reducer
    /// </summary>
    /// <typeparam name="RetT">Return type of the reducer</typeparam>
    /// <param name="_Pred">The reducer (acc, log) => ret</param>
    /// <returns>The result from the refucer</returns>
    static public RetT AllLogAnalyse<RetT>(Func<RetT, ComabatLog, RetT> _Pred) where RetT : new()
    {
        RetT ret = new();
        foreach (var log in Logs)
        {
            ret = _Pred(ret, log);
        }
        return ret;
    }
}