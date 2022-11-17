using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerCombatant : BasicCombatant
{
    public override void DeathAction(BasicCombatant _Killer)
    {
        Debug.Log("Player killed by" + _Killer.gameObject.name);
        return;
    }
}