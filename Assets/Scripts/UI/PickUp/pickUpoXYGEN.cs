using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpoXYGEN : MonoBehaviour
{
    public static pickUpoXYGEN Instance; //Instantiating the pickUpOXYGEN.cs

    [SerializeField] private GameObject oxygentank;
    private bool isTriggered = false;

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowpickUpOXYGENInfo();
        isTriggered = true;
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HidepickUpOXYGENInfo();
        isTriggered = false;
    }

    private void Update()
    {
        if(isTriggered)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                UIManager.Instance.pickUpox = true;
                Player.Instance.pickupOxygenTank();
                UIManager.Instance.HidepickUpOXYGENInfo();
                gameObject.SetActive(false);
            }
        }
    }
}
