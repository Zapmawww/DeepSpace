using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class DataManagementTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Item x = new ExampleItem();
        if (x is StackableItem)
        {
            StackableItem s = (StackableItem)x;
            Debug.Log(s.Add(8));
            Debug.Log(s.Add(8));
            Debug.Log(s.Add(18));
            Debug.Log(s.Add(18));
        }
        if (x is IUsable)
        {
            IUsable s = (IUsable)x;
            s.Use();
        }
        if (x is ChargeableItem)
        {
            ChargeableItem s = (ChargeableItem)x;
            Debug.Log(s.Charge(8));
            Debug.Log(s.Charge(8));
            Debug.Log(s.Charge(18));
            Debug.Log(s.Charge(18));
        }

        Inventory inv = new(3);
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
        Debug.Log("removed:");
        Debug.Log(inv.RemoveItem(0, 10));
        inv.SplitItem(0, 1);
        inv.AddCap(1);

        CraftingSystem.Craft(ExampleRecipe.recipe, inv);

        // basic combatant is actually armored, damage invoked correctly
        var cbt = gameObject.GetComponent<BasicCombatant>();
        if (cbt != null)
        {
            Debug.Log("Combat test:");
            Debug.Log(cbt.HitPoint);
            CombatSystem.AddCombatAct(cbt, cbt, new DamageDealer { RawValue = 5 }, "self hit 5");
            Debug.Log(cbt.HitPoint);
            CombatSystem.AddCombatAct(cbt, cbt, new Healer { RawValue = 2 }, "self heal 2");
            Debug.Log(cbt.HitPoint);
            CombatSystem.AddCombatAct(cbt, cbt, new Healer { RawValue = 5 }, "self heal 5");
            Debug.Log(cbt.HitPoint);

            var rawHealCnt = CombatSystem.TargetLogAnalyse(cbt, (double acc, CombatSystem.ComabatLog log) => acc + (log.cm is Healer h ? h.RawValue : 0));
            Debug.Log(rawHealCnt);
            var finalHealCnt = CombatSystem.TargetLogAnalyse(cbt, (double acc, CombatSystem.ComabatLog log) => acc + (log.cm is Healer h ? h.FinalValue : 0));
            Debug.Log(finalHealCnt);


            CombatSystem.AddCombatAct(cbt, cbt, new DamageDealer { RawValue = 50 }, "self hit 50");

        }

    }

    // Update is called once per frame
    void Update()
    {

    }


}
