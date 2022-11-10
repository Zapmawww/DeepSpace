using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackTrigger : MonoBehaviour
{
    public static BackTrigger Instance; //Instantiating the RepairTrigger.cs
    private bool isTriggered = false;

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowBackInfo();
        isTriggered = true;
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HideBackInfo();
        isTriggered = false;
    }

    private void Update()
    {
        if (isTriggered && UIManager.Instance.pickUp002 && UIManager.Instance.pickUp001)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (UIManager.Instance.facilityFixed) //
                {
                    UIManager.Instance.ShowGameOverWin();
                }
                else
                {
                    UIManager.Instance.ShowGameOverLose();
                }
            }
        }
    }
}
