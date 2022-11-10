using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpoXYGEN : MonoBehaviour
{
    public static pickUpoXYGEN Instance; //Instantiating the pickUpOXYGEN.cs

    [SerializeField] private GameObject oxygentank;

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowpickUpOXYGENInfo();
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HidepickUpOXYGENInfo();
    }

    public void pickupOxygentank()
    {
        oxygentank.SetActive(false);
        Player.Instance.pickupOxygenTank();
    }
}
