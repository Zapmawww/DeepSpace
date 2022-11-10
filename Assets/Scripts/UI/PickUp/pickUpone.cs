using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpone : MonoBehaviour
{
    public static pickUpone Instance; //Instantiating the pickUpOXYGEN.cs

    [SerializeField] private GameObject item001;

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowpickUp001Info();
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HidepickUp001Info();
    }

    public void pickupitem001()
    {
        item001.SetActive(false);
    }
}
