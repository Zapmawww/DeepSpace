

using System;
using System.Collections.Generic;
using System.Linq;

static class CraftingSystem
{
    /// <summary>
    /// <para>Examine the ture output</para>
    /// <br>[IN 0] + [OUT 2] => [TRUEOUT 2]</br>
    /// <br>[IN 3] + [OUT 4] => [TRUEOUT 1]</br>
    /// <br>[IN 3] + [OUT 0] => [TRUEOUT -3]</br>
    /// </summary>
    /// <param name="_Rcp">The recipe</param>
    /// <returns>ture output</returns>
    static private Dictionary<Item, int> CalcDelta(Recipe _Rcp)
    {
        var delta = new Dictionary<Item, int>();
        // calculate input as -, output as +
        foreach (var outItem in _Rcp.outputs)
            delta.Add(outItem.Key, outItem.Value.Item2);
        foreach (var inItem in _Rcp.inputs)
            if (delta.ContainsKey(inItem.Key))
                delta[inItem.Key] -= inItem.Value.Item2;
            else
                delta.Add(inItem.Key, -inItem.Value.Item2);
        return delta;
    }

    /// <summary>
    /// Craft once, without check
    /// <br>Do NOT invoke directly</br>
    /// </summary>
    /// <param name="_Rcp">Recipe</param>
    /// <param name="_Src">Inventory</param>
    static private void Craft_Unchecked(Recipe _Rcp, Inventory _Src)
    {
        // remove inputs
        foreach (var inItem in _Rcp.inputs)
        {
            int need = inItem.Value.Item2;
            foreach (var item in _Src.Items)
                if (inItem.Value.Item1(item)) // Using Pred
                {
                    need -= ((StackableItem)item).Remove(need);
                }
            // Remove empty
            var itemsToRemove = _Src.Items.Where(f => f is StackableItem item && item.Stacked == 0).ToList();
            foreach (var item in itemsToRemove)
                _Src.Items.Remove(item);

        }
        // add outputs
        foreach (var outItem in _Rcp.outputs)
        {
            int left = outItem.Value.Item2;
            if (outItem.Key is StackableItem)
            {
                while (left > 0)
                {
                    StackableItem newitem = (StackableItem)outItem.Value.Item1(); // Using Gen
                    left -= newitem.Add(left);
                    _Src.AddItem(newitem);
                }
            }
            else
            {
                while (left > 0)
                {
                    _Src.AddItem(outItem.Value.Item1()); // Using Gen
                    left--;
                }
            }
        }
    }
    /// <summary>
    /// Craft once
    /// </summary>
    /// <param name="_Rcp">The recipe to craft</param>
    /// <param name="_Src">The inventory to provide inputs</param>
    static public void Craft(Recipe _Rcp, Inventory _Src)
    {
        var deltaRecipe = CalcDelta(_Rcp);
        // check input requirements (not delta)
        foreach (var inItem in _Rcp.inputs)
        {
            int need = inItem.Value.Item2;
            foreach (var item in _Src.Items)
                if (inItem.Value.Item1(item)) // Using Pred
                {
                    need -= item is StackableItem ? ((StackableItem)item).Stacked : 1;
                    if (need <= 0) break;
                }
            if (need > 0) return;// Not enough inputs
        }

        // check output space requirements (using delta)
        int spaceReq = 0;
        foreach (var outItem in deltaRecipe)
        {
            int need = outItem.Value;
            if (outItem.Key is StackableItem)
            {
                if (need > 0)
                {
                    foreach (var item in _Src.Items)
                        if (item.GetType() == outItem.Key.GetType())
                        {
                            need -= ((StackableItem)item).MaxStack - ((StackableItem)item).Stacked;
                            if (need <= 0) break;
                        }
                    if (need > 0) spaceReq += (int)Math.Ceiling((double)need / ((StackableItem)outItem.Key).MaxStack);
                }
                else if (need == 0) continue;
                else
                {
                    foreach (var item in _Src.Items)
                        if (item.GetType() == outItem.Key.GetType())
                        {
                            need += ((StackableItem)item).Stacked;
                            if (need <= 0) spaceReq--;
                            if (need >= 0) break;
                        }
                }
            }
            else
            {
                spaceReq += need;
            }
        }
        // Assertion succeed, craft
        if (_Src.CurrentSize + spaceReq <= _Src.MaxCapacity)
        {
            Craft_Unchecked(_Rcp, _Src);
        }
        else return;// No enough spaces
    }
}
