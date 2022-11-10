using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{



    public void OnTriggerEnter(Collider other)     //Show button when triggered

    {
        UIManager.Instance.ShowDoorButton();
    }

    public void OnTriggerExit(Collider other) //Hide button when not triggered
    {
        UIManager.Instance.HideDoorButton();
    }

    
}
