using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ExampleArmoredCombatant : BasicCombatant
{
    public double Armor { get; protected set; } = 1;

    override public void Damage(CombatMessenger _CM)
    {
        if (_CM.RawValue <= Armor) return;

        if (HitPoint - _CM.RawValue + Armor <= 0)
        {
            _CM.FinalValue = HitPoint;
            HitPoint = 0;
            DeathAction();
        }
        else
        {
            _CM.FinalValue = _CM.RawValue - Armor;
            HitPoint -= _CM.FinalValue;
        }
    }
}