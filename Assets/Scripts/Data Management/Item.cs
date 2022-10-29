
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine.SocialPlatforms;

/// <summary>
/// Basic Item class, implement this while creating any item class
/// </summary>
abstract class Item
{
    protected Item(Item _Item)
    {
        Name = _Item.Name;
        ID = _Item.ID;
        Descriptor = _Item.Descriptor;
    }
    protected Item() { }
    public string Name { get; }
    public int ID { get; }

    public Descriptor Descriptor { get; }

    /// <summary>
    /// Get a copy of an Item
    /// </summary>
    /// <returns>The copied</returns>
    public abstract Item Clone();
}

/// <summary>
/// Chargeable Item (fuel, power, ...)
/// </summary>
abstract class ChargeableItem : Item
{
    protected ChargeableItem(ChargeableItem _Item) : base(_Item)
    {
        Charged = _Item.Charged;
    }
    public ChargeableItem() : base() { }

    /// <summary>
    /// Max amount of fully charged
    /// </summary>
    public abstract int MaxCharge { get; }
    /// <summary>
    /// Current Status
    /// </summary>
    public int Charged { get; private set; }
    /// <summary>
    /// Charge the item
    /// </summary>
    /// <param name="_Amount">How many is supposed to be charge</param>
    /// <returns>Actually amount charged (before overflow)</returns>
    public int Charge(int _Amount)
    {
        if (_Amount + Charged > MaxCharge)
        {
            var ret = MaxCharge - Charged;
            Charged = MaxCharge;
            return ret;
        }
        else
        {
            Charged += _Amount;
            return _Amount;
        }
    }
    /// <summary>
    /// Discharge the item
    /// </summary>
    /// <param name="_Amount">How many is supposed to be discharge</param>
    /// <returns>Actually amount discharged (before fully discharged)</returns>
    public int Discharge(int _Amount)
    {
        if (Charged - _Amount < 0)
        {
            Charged = 0;
            return Charged;
        }
        else
        {
            Charged -= _Amount;
            return _Amount;
        }
    }
}

/// <summary>
/// Stackabe in inventory
/// </summary>
abstract class StackableItem : Item
{
    protected StackableItem(StackableItem _Item) : base(_Item)
    {
        Stacked = _Item.Stacked;
    }
    public StackableItem() : base() { }

    /// <summary>
    /// Stack limit
    /// </summary>
    public abstract int MaxStack { get; }
    /// <summary>
    /// Current status
    /// </summary>
    public int Stacked { get; private set; }

    /// <summary>
    /// Add some to this stack
    /// </summary>
    /// <param name="_Amount">How many is supposed to be added</param>
    /// <returns>Actually amount added (before Exceeding the stack limit)</returns>
    public int Add(int _Amount)
    {
        if (_Amount + Stacked > MaxStack)
        {
            var ret = MaxStack - Stacked;
            Stacked = MaxStack;
            return ret;
        }
        else
        {
            Stacked += _Amount;
            return _Amount;
        }
    }
    /// <summary>
    /// Remove some from this stack
    /// </summary>
    /// <param name="_Amount">How many is supposed to be removed</param>
    /// <returns>Actually amount removed (before nothing is left)</returns>
    public int Remove(int _Amount)
    {
        if (Stacked - _Amount < 0)
        {
            Stacked = 0;
            return Stacked;
        }
        else
        {
            Stacked -= _Amount;
            return _Amount;
        }
    }
}

/// <summary>
/// Usable Item, a basic function for use
/// </summary>
interface IUsable
{
    /// <summary>
    /// Use this item, actual behavior implemented in derived classes
    /// </summary>
    /// <returns>If successfully used</returns>
    bool Use();
}


class ExampleItem : StackableItem, IUsable
{
    public ExampleItem(ExampleItem _Item) : base(_Item)
    { }

    public ExampleItem() : base() { }

    public override int MaxStack => 2;


    /// <summary>
    /// ExampleItem Use()
    /// </summary>
    /// <returns>true</returns>
    public bool Use()
    {
        UnityEngine.Debug.Log("ExampleItem Used");
        return true;
    }
    /// <summary>
    /// Copy this ExampleItem
    /// </summary>
    /// <returns>A new ExampleItem stores the same data from this</returns>
    public override Item Clone()
    {
        return new ExampleItem(this);
    }
}
