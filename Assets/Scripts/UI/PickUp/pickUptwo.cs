using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUptwo : MonoBehaviour
{
    public static pickUptwo Instance; //Instantiating the pickUpOXYGEN.cs

    [SerializeField] private GameObject item002;

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowpickUp002Info();
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HidepickUp002Info();
    }

    public void pickupitem002()
    {
        item002.SetActive(false);
    }
}
