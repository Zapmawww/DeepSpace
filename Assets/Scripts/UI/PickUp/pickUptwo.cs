using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUptwo : MonoBehaviour
{
    public static pickUptwo Instance; //Instantiating the pickUpOXYGEN.cs
    private bool isTriggered = false;

    [SerializeField] private GameObject item002;

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowpickUp002Info();
        isTriggered = true;
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HidepickUp002Info();
        isTriggered = false;
    }

    private void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                UIManager.Instance.pickUp002 = true;
                UIManager.Instance.HidepickUp002Info();
                gameObject.SetActive(false);
            }
        }
    }
}
