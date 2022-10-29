

using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using static UnityEditor.Progress;

class Inventory
{
    /// <summary>
    /// The list of items
    /// </summary>
    public List<Item> Items { get; } = null;
    /// <summary>
    /// Max number of items allowed to store
    /// </summary>
    public int MaxCapacity { get; private set; }
    /// <summary>
    /// How many items are stored currently
    /// </summary>
    public int CurrentSize { get { return Items.Count; } }

    /// <summary>
    /// Construct a new inventory with specific max capacity
    /// </summary>
    /// <param name="_MaxCap">Max capacity of the inventory</param>
    public Inventory(int _MaxCap)
    {
        MaxCapacity = _MaxCap;
        Items = new();
    }
    /// <summary>
    /// Add more capacity to the inventory
    /// </summary>
    /// <param name="_Add">How many to add</param>
    public void AddCap(int _Add)
    {
        MaxCapacity += _Add;
    }
    /// <summary>
    /// Add an item to the inventory
    /// </summary>
    /// <param name="_Item">The item to add into</param>
    /// <returns>null if succeed, an remaining item if the inventory is full</returns>
    public Item AddItem(Item _Item)
    {
        if (_Item is StackableItem)
        {
            int index = 0;
            while (true)
            {
                index = Items.FindIndex(index, item => item.ID == _Item.ID);
                if (index == -1) break;
                ((StackableItem)_Item).Remove(
                    ((StackableItem)Items[index]).Add(((StackableItem)_Item).Stacked)
                    );
                if (((StackableItem)_Item).Stacked == 0) return null;
                index++;
            }
        }

        if (CurrentSize == MaxCapacity) return _Item;
        Items.Add(_Item);
        return null;
    }
    /// <summary>
    /// Remove a specific amount (default 1) of item at specific location
    /// </summary>
    /// <param name="_Index">Which item to be removed</param>
    /// <param name="_Amount">How many (if stackable) items to be removed</param>
    /// <returns></returns>
    public int RemoveItem(int _Index, int _Amount = 1)
    {
        var item = Items[_Index];
        if (item is not StackableItem) Items.RemoveAt(_Index);
        else
        {
            StackableItem si = (StackableItem)item;
            if (_Amount >= si.Stacked) { int ret = si.Stacked; Items.RemoveAt(_Index); return ret; }
        }
        return _Amount;
    }
    /// <summary>
    /// Split a specific amount of StackableItem from another StackableItem
    /// </summary>
    /// <param name="_Index">Which item to split</param>
    /// <param name="_Amount">How many items to split</param>
    public void SplitItem(int _Index, int _Amount)
    {
        var item = Items[_Index];
        if (item is not StackableItem) return;

        StackableItem si = (StackableItem)item;
        if (CurrentSize == MaxCapacity) return;
        if (_Amount >= si.Stacked) return;

        Item newItem = si.Clone();
        si.Remove(_Amount);
        ((StackableItem)newItem).Remove(si.Stacked);
        Items.Add((StackableItem)newItem);
    }
}