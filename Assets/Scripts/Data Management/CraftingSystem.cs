

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using static UnityEditor.Progress;

abstract class Recipe
{
    /// <summary>
    /// Recipe Name
    /// </summary>
    public abstract string Name { get; }
    /// <summary>
    /// Recipe Id
    /// </summary>
    public abstract int RecipeId { get; }
    public abstract string Description { get; }
    /// <summary>
    /// The intake of this recipe, <ItemId, ItemCount>
    /// </summary>
    public abstract Dictionary<Item, int> Inputs { get; }
    /// <summary>
    /// The outcome of this recipe, <ItemId, ItemCount>
    /// </summary>
    public abstract Dictionary<Item, int> Outputs { get; }
    /// <summary>
    /// Time (in seconds) to complete this recipe
    /// </summary>
    public abstract int Time { get; }
}

static class CraftingSystem
{
    /// <summary>
    /// <para>Examine the ture output</para>
    /// <br>[IN 3] + [OUT 2] => [TRUEOUT -1]</br>
    /// <br>[IN 3] + [OUT 4] => [TRUEOUT 1]</br>
    /// </summary>
    /// <param name="_Rcp">The recipe</param>
    /// <returns>ture output</returns>
    static private Dictionary<Item, int> CalcTrueOut(Recipe _Rcp)
    {
        var trueOut = new Dictionary<Item, int>(_Rcp.Outputs);

        foreach (var inItem in _Rcp.Inputs)
            if (_Rcp.Outputs.ContainsKey(inItem.Key))
                trueOut[inItem.Key] -= inItem.Value;

        return trueOut;
    }

    /// <summary>
    /// Craft once, without check
    /// <br>Do NOT invoke directly</br>
    /// </summary>
    /// <param name="_Rcp">Recipe</param>
    /// <param name="_Src">Inventory</param>
    static private void Craft_Unchecked(Recipe _Rcp, Inventory _Src)
    {
        foreach (var inItem in _Rcp.Inputs)
        {
            int need = inItem.Value;
            foreach (var item in _Src.Items)
                if (item.GetType() == inItem.Key.GetType())
                {
                    need -= ((StackableItem)item).Remove(need);
                }
            // Remove empty
            var itemsToRemove = _Src.Items.Where(f => f is StackableItem item && item.Stacked == 0);
            foreach (var item in itemsToRemove)
                _Src.Items.Remove(item);

        }
        foreach (var outItem in _Rcp.Outputs)
        {
            int left = outItem.Value;
            if (outItem.Key is StackableItem)
            {
                while (left > 0)
                {
                    StackableItem newitem = (StackableItem)outItem.Key.Clone();
                    left -= newitem.Add(left);
                    _Src.AddItem(newitem);
                }
            }
            else
            {
                while (left > 0)
                {
                    _Src.AddItem(outItem.Key.Clone());
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
        var trueOut = CalcTrueOut(_Rcp);

        foreach (var inItem in _Rcp.Inputs)
        {
            int need = inItem.Value;
            foreach (var item in _Src.Items)
                if (item.GetType() == inItem.Key.GetType())
                {
                    need -= item is StackableItem ? ((StackableItem)item).Stacked : 1;
                    if (need <= 0) break;
                }
            if (need > 0) return;// Not enough inputs
        }

        int spaceReq = 0;
        foreach (var outItem in trueOut)
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
        if (_Src.CurrentSize + spaceReq <= _Src.MaxCapacity)
        {
            Craft_Unchecked(_Rcp, _Src);
        }
        else return;// Not enough spaces
    }
}
class ExampleRecipe : Recipe
{
    public override string Name => "Example_R";

    public override int RecipeId => 1;

    public override string Description => "An Example Recipe";

    public override Dictionary<Item, int> Inputs => new Dictionary<Item, int> { { new ExampleItem(), 2 } };

    public override Dictionary<Item, int> Outputs => new Dictionary<Item, int> { { new ExampleChargeableItem(), 2 } };

    public override int Time => 10;
}