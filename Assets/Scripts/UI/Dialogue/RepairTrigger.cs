using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairTrigger : MonoBehaviour
{
    public static RepairTrigger Instance; //Instantiating the RepairTrigger.cs
    private bool isTriggered = false;

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowRepairInfo();
        isTriggered = true;
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HideRepairInfo();
        isTriggered = false;
    }

    private void Update()
    {
        if (isTriggered && UIManager.Instance.pickUp002 && UIManager.Instance.pickUp001)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                UIManager.Instance.facilityFixed = true;
                UIManager.Instance.HideRepairInfo();
                gameObject.SetActive(false);
            }
        }
    }
}
