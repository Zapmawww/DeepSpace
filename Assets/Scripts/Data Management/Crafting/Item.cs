
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Basic Item class, implement this while creating any item class
/// </summary>
public abstract class Item
{
    protected Item(Item _Item)
    { }
    protected Item() { }
    public abstract string Name { get; }
    public abstract int ID { get; }

    public abstract Descriptor Descriptor { get; }

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
    public int Charged { get; private set; } = 0;
    /// <summary>
    /// Charge the item
    /// </summary>
    /// <param name="_Amount">How many is supposed to be charge, no effects if less than 0</param>
    /// <returns>Actually amount charged (before overflow)</returns>
    public int Charge(int _Amount)
    {
        if (_Amount <= 0) return 0;
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
    /// <param name="_Amount">How many is supposed to be discharge, no effects if less than 0</param>
    /// <returns>Actually amount discharged (before fully discharged)</returns>
    public int Discharge(int _Amount)
    {
        if (_Amount <= 0) return 0;
        if (Charged - _Amount < 0)
        {
            var ret = Charged;
            Charged = 0;
            return ret;
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
    public int Stacked { get; private set; } = 0;

    /// <summary>
    /// Add some to this stack
    /// </summary>
    /// <param name="_Amount">How many is supposed to be added, no effects if less than 0</param>
    /// <returns>Actually amount added (before Exceeding the stack limit)</returns>
    public int Add(int _Amount)
    {
        if (_Amount <= 0) return 0;
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
    /// <param name="_Amount">How many is supposed to be removed, no effects if less than 0</param>
    /// <returns>Actually amount removed (before nothing is left)</returns>
    public int Remove(int _Amount)
    {
        if (_Amount <= 0) return 0;
        if (Stacked - _Amount < 0)
        {
            var ret = Stacked;
            Stacked = 0;
            return ret;
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

    public ExampleItem() : base()
    { }

    public override string Name => "Example";
    public override int ID => 1;
    public override int MaxStack => 2;

    static Descriptor descriptor = new Descriptor
    {
        brief = "An example item",
        properties = new Dictionary<string, string>
        {
            { "Item_Size", "example size" },
            { "Detailed_IUsable", "use a example item" },
            { "Detailed_StackableItem", "stack up to 2" }
        }
    };
    public override Descriptor Descriptor => descriptor;

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

class ExampleChargeableItem : ChargeableItem
{
    public ExampleChargeableItem(ExampleChargeableItem _Item) : base(_Item)
    { }
    public ExampleChargeableItem() : base()
    { }

    public override int MaxCharge => 100;
    public override string Name => "Chargeable1";
    public override int ID => 2;

    static Descriptor descriptor = new Descriptor
    {
        brief = "An example item",
        properties = new Dictionary<string, string>
        {
            { "Detailed_Info", "Super duper charging item with 100 charges." },
            { "Detailed_ChargeableItem", "charged up to 100" }
        }
    };
    public override Descriptor Descriptor => descriptor;
    /// <summary>
    /// Copy this ExampleChargeableItem
    /// </summary>
    /// <returns>A new ExampleItem stores the same data from this</returns>
    public override Item Clone()
    {
        return new ExampleChargeableItem(this);
    }
}