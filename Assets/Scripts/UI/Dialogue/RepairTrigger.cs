using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTrigger : MonoBehaviour
{
    public static RepairTrigger Instance; //Instantiating the RepairTrigger.cs

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowRepairInfo();
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HideRepairInfo();
    }
}
