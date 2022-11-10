using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTrigger : MonoBehaviour
{
    public static BackTrigger Instance; //Instantiating the RepairTrigger.cs

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowBackInfo();
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HideBackInfo();
    }
}
