using UnityEngine;

/// <summary>
/// A really basic combatant model contains HP and MaxHP. Add/Modify by deriving new classes. 
/// Note: used for units receiving damage.
/// </summary>
class BasicCombatant : MonoBehaviour
{
    public double maxHP = 10;
    public double HitPoint { get; protected set; }
    public double MaxHitPoint { get => maxHP; protected set => maxHP = value; }

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
    virtual protected void Heal(CombatMessenger _CM)
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
    virtual protected void Damage(CombatMessenger _CM)
    {
        if (_CM.RawValue == 0) return;

        if (HitPoint - _CM.RawValue <= 0)
        {
            _CM.FinalValue = HitPoint;
            DeathAction();
        }
        else
            _CM.FinalValue = _CM.RawValue;
        HitPoint -= _CM.FinalValue;
    }
    /// <summary>
    /// Simply invoke heal or damage according to the CombatMessenger type
    /// </summary>
    /// <param name="_CM">The CombatMessenger that contains the combat info</param>
    /// <returns></returns>
    virtual public bool ReceiveCM(CombatMessenger _CM)
    {
        if (_CM is Healer)
        {
            Heal(_CM);
            return true;
        }
        else if (_CM is DamageDealer)
        {
            Damage(_CM);
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// To kill the combatant. Modify by deriving new classes.
    /// </summary>
    virtual public void DeathAction()
    {
        gameObject.SetActive(false);
    }
}

