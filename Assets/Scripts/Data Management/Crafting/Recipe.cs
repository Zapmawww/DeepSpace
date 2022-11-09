
using System.Collections.Generic;
using System;

struct Recipe
{
    /// <summary>
    /// Recipe Name
    /// </summary>
    public string name;
    /// <summary>
    /// Recipe Id
    /// </summary>
    public int recipeId;
    /// <summary>
    /// Recipe Description
    /// </summary>
    public string description;
    /// <summary>
    /// The intake of this recipe, [ItemId, [ItemPred, ItemCount]]
    /// </summary>
    public Dictionary<Item, Tuple<Func<Item, bool>, int>> inputs;
    /// <summary>
    /// The outcome of this recipe, [ItemId, [ItemGen, ItemCount]]
    /// </summary>
    public Dictionary<Item, Tuple<Func<Item>, int>> outputs;
    /// <summary>
    /// Time (in seconds) to complete this recipe
    /// </summary>
    public int time;
    /// <summary>
    /// Prerequisite storyline level
    /// </summary>
    public int storyLineLevel;
}

/// <summary>
/// Example Recipe for tests
/// </summary>
class ExampleRecipe
{
    public static readonly Recipe recipe = new()
    {
        name = "Example_R",
        recipeId = 1,
        description = "An Example Recipe",
        inputs = new() {
            { new ExampleItem(), new((Item f) => f is ExampleItem i && i.Stacked == 1, 2) }
        },
        outputs = new() {
            { new ExampleChargeableItem(), new(() => new ExampleChargeableItem(), 2) }
        },
        time = 10,
        storyLineLevel = 12,
    };
}