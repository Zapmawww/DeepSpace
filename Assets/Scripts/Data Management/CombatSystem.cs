
using System;
using System.Collections.Generic;



static class CombatSystem
{
    public class ComabatLog
    {
        public BasicCombatant source;
        public CombatMessenger cm;
        public BasicCombatant target;
        public string extraInfo;
        public DateTime time;
    }

    static public List<ComabatLog> Logs { get; private set; } = new();
    static public Dictionary<BasicCombatant, List<ComabatLog>> AsTarget { get; private set; } = new();
    static public Dictionary<BasicCombatant, List<ComabatLog>> AsSource { get; private set; } = new();

    static public void Clean()
    {
        AsTarget.Clear();
        AsSource.Clear();
        Logs.Clear();
    }

    static public void AddCombatAct(BasicCombatant _Source, BasicCombatant _Target, CombatMessenger _CM, string _Info)
    {
        //act
        if (_CM is DamageDealer)
            _Target.Damage(_CM);
        else if (_CM is Healer)
            _Target.Heal(_CM);

        //log
        var log = new ComabatLog { source = _Source, target = _Target, cm = _CM, extraInfo = _Info, time = DateTime.Now };
        Logs.Add(log);

        if (!AsTarget.ContainsKey(_Target))
            AsTarget.Add(_Target, new());
        AsTarget[_Target].Add(log);

        if (!AsSource.ContainsKey(_Source))
            AsSource.Add(_Source, new());
        AsSource[_Source].Add(log);
    }
    static public RetT TargetLogAnalyse<RetT>(BasicCombatant _Target, Func<RetT, ComabatLog, RetT> _Pred) where RetT : new()
    {
        RetT ret = new();
        foreach (var log in AsTarget[_Target])
        {
            ret = _Pred(ret, log);
        }
        return ret;
    }
    static public RetT SourceLogAnalyse<RetT>(BasicCombatant _Source, Func<RetT, ComabatLog, RetT> _Pred) where RetT : new()
    {
        RetT ret = new();
        foreach (var log in AsSource[_Source])
        {
            ret = _Pred(ret, log);
        }
        return ret;
    }
}