
/// <summary>
/// Used for transfer combat info from a combatant to another
/// </summary>
abstract public class CombatMessenger
{
    /// <summary>
    /// Raw damage, heal, buff time, etc.
    /// </summary>
    public double RawValue;
    /// <summary>
    /// Actual value that take effects
    /// </summary>
    public double FinalValue = 0;
}

class Healer : CombatMessenger
{

}

class DamageDealer : CombatMessenger
{

}

class Buffs : CombatMessenger
{
    public string buffName;
    public float duration;
}