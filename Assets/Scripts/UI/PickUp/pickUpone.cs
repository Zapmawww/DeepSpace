using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUpone : MonoBehaviour
{
    public static pickUpone Instance; //Instantiating the pickUpOXYGEN.cs
    private bool isTriggered = false;

    [SerializeField] private GameObject item001;

    void Awake()
    {
        Instance = this;
    }


    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowpickUp001Info();
        isTriggered = true;
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HidepickUp001Info();
        isTriggered = false;
    }

    private void Update()
    {
        if (isTriggered)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                UIManager.Instance.bag001Info.SetActive(true);
                UIManager.Instance.pickUp001 = true;
                UIManager.Instance.HidepickUp001Info();
                gameObject.SetActive(false);
            }
        }
    }
}
