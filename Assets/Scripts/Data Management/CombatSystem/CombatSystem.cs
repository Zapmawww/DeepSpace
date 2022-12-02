
using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using Vector3 = UnityEngine.Vector3;

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
        public CombatActionReturn result;
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
    /// Contain all callback functions used to check the combat action before it is applied
    /// </summary>
    static public List<Func<BasicCombatant, BasicCombatant, CombatMessenger, bool>> PriorFuncs { get; private set; } = new();
    /// <summary>
    /// Contain all callback functions used to check the combat action after it is applied
    /// </summary>
    static public List<Func<BasicCombatant, BasicCombatant, CombatMessenger, CombatActionReturn, bool>> LaterFuncs { get; private set; } = new();
    /// <summary>
    /// Clean all combat logs, e.g. after scene switching
    /// </summary>
    static public void Clean()
    {
        PriorFuncs.Clear();
        LaterFuncs.Clear();

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
    /// <returns>Action result</returns>
    static public CombatActionReturn AddCombatAct(BasicCombatant _Source, BasicCombatant _Target, CombatMessenger _CM)
    {
        //prior callbacks
        PriorFuncs.RemoveAll(func => func(_Source, _Target, _CM) == true);

        //act
        var ret = _Target.ReceiveCM(_Source, _CM);

        //log
        var log = new ComabatLog { source = _Source, target = _Target, cm = _CM, extraInfo = null, time = DateTime.Now, result = ret };
        Logs.Add(log);
        if (!AsTarget.ContainsKey(_Target))
            AsTarget.Add(_Target, new());
        AsTarget[_Target].Add(log);

        if (!AsSource.ContainsKey(_Source))
            AsSource.Add(_Source, new());
        AsSource[_Source].Add(log);

        //killed log
        if (ret.killed)
        {
            var killLog = new ComabatLog { source = _Source, target = _Target, cm = _CM, extraInfo = "KILL", time = DateTime.Now, result = ret };
            Logs.Add(killLog);
            AsTarget[_Target].Add(killLog);
            AsSource[_Source].Add(killLog);
        }

        //later callbacks
        LaterFuncs.RemoveAll(func => func(_Source, _Target, _CM, ret) == true);

        return ret;
    }
    /// <summary>
    /// Apply a AOE at given position
    /// </summary>
    /// <param name="_Source">The source combatant who starts the action</param>
    /// <param name="_CMFactory">A function generates combat messengers</param>
    /// <param name="_Position">The center coordinate of the AOE action</param>
    /// <param name="_Rad">The radius of the action</param>
    /// <param name="_Coverable">Can this action be covered by protections (e.g. walls)</param>
    /// <returns>All action results (if hit any combatant)</returns>
    static public List<CombatActionReturn> AddAOECombatAct(BasicCombatant _Source, Func<BasicCombatant, BasicCombatant, CombatMessenger> _CMFactory, Vector3 _Position, float _Rad, bool _Coverable)
    {
        var allColliders = Physics.OverlapSphere(_Position, _Rad);
        List<CombatActionReturn> ret = new();

        foreach (var collider in allColliders)
        {
            // check if a combatant
            var cbt = collider.gameObject.GetComponent<BasicCombatant>();
            if (cbt != null)
            {
                // test if covered by blindages
                if (_Coverable)
                {
                    RaycastHit hit;
                    if (Physics.Raycast(_Position, (collider.gameObject.transform.position - _Position), out hit, _Rad))
                    {
                        // test failed
                        if (hit.collider == collider) continue;
                    }
                }
                ret.Add(AddCombatAct(_Source, cbt, _CMFactory(_Source, cbt)));
            }
        }
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