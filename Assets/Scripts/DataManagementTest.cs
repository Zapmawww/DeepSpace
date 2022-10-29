using System.Collections;
using System.Collections.Generic;
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


    }

    // Update is called once per frame
    void Update()
    {

    }


}
