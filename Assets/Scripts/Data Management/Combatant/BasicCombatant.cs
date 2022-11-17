using UnityEngine;

/// <summary>
/// The return value of a combat action. Communicate with Combat System.
/// </summary>
class CombatActionReturn
{
    /// <summary>
    /// Indicate if the action is valid to the combatant.
    /// </summary>
    public bool valid = false;
    /// <summary>
    /// Indicate if the action killed the combatant.
    /// </summary>
    public bool killed = false;
    /// <summary>
    /// Why the action is invalid.
    /// </summary>
    public string invalidReason = null;

}

/// <summary>
/// A really basic combatant model contains HP and MaxHP. Add/Modify by deriving new classes. 
/// Note: used for units receiving damage.
/// </summary>
class BasicCombatant : MonoBehaviour
{
    public double maxHP = 10;
    public double HitPoint { get; protected set; }
    public double MaxHitPoint { get => maxHP; protected set => maxHP = value; }
    public bool IsDead { get; protected set; }

    /// <summary>
    /// Initialization
    /// </summary>
    public void Awake()
    {
        HitPoint = maxHP;
    }

    /// <summary>
    /// Simple healing algorithm. Modify by deriving new classes.
    /// </summary>
    /// <param name="_CM">make sure RAW healing is passed in</param>
    /// <param name="_Source">the source combatant</param>
    virtual protected void Heal(BasicCombatant _Source, CombatMessenger _CM)
    {
        if (_CM.RawValue == 0) return;

        if (HitPoint + _CM.RawValue > MaxHitPoint)
            _CM.FinalValue = MaxHitPoint - HitPoint;
        else _CM.FinalValue = _CM.RawValue;

        HitPoint += _CM.FinalValue;
    }
    /// <summary>
    /// Simple damage algorithm. Modify by deriving new classes.
    /// </summary>
    /// <param name="_CM">make sure RAW damage is passed in</param>
    /// <param name="_Source">the source combatant</param>
    virtual protected void Damage(BasicCombatant _Source, CombatMessenger _CM)
    {
        if (_CM.RawValue == 0) return;

        if (HitPoint - _CM.RawValue <= 0)
        {
            _CM.FinalValue = HitPoint;
            DeathAction(_Source);
        }
        else
            _CM.FinalValue = _CM.RawValue;
        HitPoint -= _CM.FinalValue;
    }
    /// <summary>
    /// Simply invoke heal or damage according to the CombatMessenger type. Modify by deriving new classes.
    /// </summary>
    /// <param name="_CM">The CombatMessenger that contains the combat info</param>
    /// <param name="_Source">the source combatant</param>
    /// <returns></returns>
    virtual public CombatActionReturn ReceiveCM(BasicCombatant _Source, CombatMessenger _CM)
    {
        if (IsDead) return new CombatActionReturn { valid = false, killed = false, invalidReason = "Already dead." };
        if (_CM is Healer)
        {
            Heal(_Source, _CM);
            return new CombatActionReturn { valid = true, killed = false };
        }
        else if (_CM is DamageDealer)
        {
            Damage(_Source, _CM);
            return new CombatActionReturn { valid = true, killed = IsDead };
        }
        else
        {
            return new CombatActionReturn { valid = false, killed = false, invalidReason = "Unsupported action." };
        }
    }

    /// <summary>
    /// To kill the combatant. Modify by deriving new classes.
    /// </summary>
    virtual public void DeathAction(BasicCombatant _Killer)
    {
        gameObject.SetActive(false);
    }
}

