using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DataManagementTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Test Item classes
        Item x = new ExampleItem();
        // Stack properly
        if (x is StackableItem)
        {
            StackableItem s = (StackableItem)x;
            Debug.Log(s.Add(8));
            Debug.Log(s.Add(8));
            Debug.Log(s.Add(18));
            Debug.Log(s.Add(18));
        }
        // Use properly
        if (x is IUsable)
        {
            IUsable s = (IUsable)x;
            s.Use();
        }
        // Charge properly
        if (x is ChargeableItem)
        {
            ChargeableItem s = (ChargeableItem)x;
            Debug.Log(s.Charge(8));
            Debug.Log(s.Charge(8));
            Debug.Log(s.Charge(18));
            Debug.Log(s.Charge(18));
        }

        // Inventory creation
        Inventory inv = new(3);

        // Add items into Inventory
        for (int i = 0; i < 10; i++)
        {
            ExampleItem example = new();
            example.Add(1);
            var ret = inv.AddItem(example);
            if (ret != null)
            {
                Debug.Log(ret);
            }
            else
            {
                Debug.Log("succeed");
            }
        }
        // Remove a type of item at a number for stackable
        Debug.Log("removed:");
        Debug.Log(inv.RemoveItem(0, 10));
        // split a stackable to two halfs
        inv.SplitItem(0, 1);
        // add capacity for the inv
        inv.AddCap(1);

        // Test craft and recipe system
        CraftingSystem.Craft(ExampleRecipe.recipe, inv);

        // Basic combatant is actually armored, Damage() invoked correctly
        var cbt = gameObject.GetComponent<BasicCombatant>();
        if (cbt != null)
        {
            // AddCombatAct and Messenger test
            Debug.Log("Combat test:");
            Debug.Log(cbt.HitPoint);
            CombatSystem.AddCombatAct(cbt, cbt, new DamageDealer { RawValue = 5 }, "self hit 5");
            Debug.Log(cbt.HitPoint);
            CombatSystem.AddCombatAct(cbt, cbt, new Healer { RawValue = 2 }, "self heal 2");
            Debug.Log(cbt.HitPoint);
            CombatSystem.AddCombatAct(cbt, cbt, new Healer { RawValue = 5 }, "self heal 5");
            Debug.Log(cbt.HitPoint);

            bool ret = CombatSystem.AddCombatAct(cbt, cbt, new Buffs { RawValue = 5, buffName="test Buff" }, "add a buff for 5 seconds");
            Debug.Log(ret);// should be false

            // LogAnalyse test
            var rawHealCnt = CombatSystem.TargetLogAnalyse(cbt, (double acc, CombatSystem.ComabatLog log) => acc + (log.cm is Healer h ? h.RawValue : 0));
            Debug.Log(rawHealCnt);
            var finalHealCnt = CombatSystem.TargetLogAnalyse(cbt, (double acc, CombatSystem.ComabatLog log) => acc + (log.cm is Healer h ? h.FinalValue : 0));
            Debug.Log(finalHealCnt);
            var invalidCnt = CombatSystem.TargetLogAnalyse(cbt, (int acc, CombatSystem.ComabatLog log) => acc + (log.succeeded ? 0 : 1));
            Debug.Log(invalidCnt);

            // Death action test
            CombatSystem.AddCombatAct(cbt, cbt, new DamageDealer { RawValue = 50 }, "self hit 50");

            CombatSystem.Clean();
        }

    }

    // Update is called once per frame
    void Update()
    {

    }


}
